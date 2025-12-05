using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Services.Utils;
using System.Security.Claims; // Necessário para ler Claims padrão se preciso

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TimeRecordController : Controller
{
    private readonly ITimeRecordService _timeRecordService;
    private readonly IWorkScheduleService _workScheduleService;
    private readonly IGeofenceService _geofenceService;
    private readonly IUserService _userService;
    private readonly IDailyRecordService _dailyRecordService;
    private readonly Response _response;

    public TimeRecordController(ITimeRecordService timeRecordService, IWorkScheduleService workScheduleService, IGeofenceService geofenceService, IUserService userService, IDailyRecordService dailyRecordService)
    {
        _timeRecordService = timeRecordService;
        _workScheduleService = workScheduleService;
        _geofenceService = geofenceService;
        _userService = userService;
        _dailyRecordService = dailyRecordService;
        _response = new Response();
    }

    // --- MÉTODO GETALL MODIFICADO ---
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            // 1. Tenta pegar o ID do Token (claim "id")
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            // Se não achar "id", tenta o padrão do .NET "NameIdentifier"
            if (string.IsNullOrEmpty(userIdClaim))
            {
                userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            if (string.IsNullOrEmpty(userIdClaim))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Message = "Token inválido: Não foi possível identificar o usuário.";
                return Unauthorized(_response);
            }

            int userId = int.Parse(userIdClaim);

            // 2. Chama o método que filtra pelo ID do usuário
            var timeRecordsDTO = await _timeRecordService.GetByUserId(userId);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = timeRecordsDTO;
            _response.Message = "Marcações de ponto listadas com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao listar histórico";
            _response.Data = new { Error = ex.Message };
            return BadRequest(_response);
        }
    }
    // --------------------------------

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var timeRecordDTO = await _timeRecordService.GetById(id);

        if (timeRecordDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "Marcação de ponto não encontrada";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = timeRecordDTO;
        _response.Message = "Marcação de ponto listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TimeRecordDTO timeRecordDTO)
    {
        if (timeRecordDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        // 1. Validação de GPS
        if (!GeoUtils.IsValidGeolocation(timeRecordDTO.Latitude, timeRecordDTO.Longitude))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Formato das coordenadas de geolocalização incorreto";
            return BadRequest(_response);
        }

        try
        {
            // Validação de Usuário Ativo
            if (!await _userService.IsUserActive(timeRecordDTO.UserId))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "Usuário inativo ou não encontrado";
                return BadRequest(_response);
            }

            var serverNow = DateTime.Now;
            var serverDate = DateOnly.FromDateTime(serverNow);
            var userId = timeRecordDTO.UserId;

            // Busca escala
            var allSchedules = await _workScheduleService.GetAll();

            var schedule = allSchedules.FirstOrDefault(w =>
                w.UserId == userId &&
                w.YearMonth == serverDate.ToString("yyyy/MM") &&
                w.DayOfMonth == serverDate.Day
            );

            if (schedule == null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = $"Não há escala de trabalho configurada para o dia {serverDate:dd/MM/yyyy}.";
                return NotFound(_response);
            }

            // 3. Validação da Geofence
            bool isInsideGeofence = await _geofenceService.IsInsideGeofence(
                timeRecordDTO.Latitude,
                timeRecordDTO.Longitude,
                schedule.GeofenceId
            );

            if (!isInsideGeofence)
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "Localização atual não corresponde à área permitida (Fora da cerca virtual).";
                return BadRequest(_response);
            }

            // 4. Salvar Ponto e Atualizar Diário
            var dailyRecordId = await _dailyRecordService.EnsureDailyRecordExists(userId, schedule.Id, serverDate);

            timeRecordDTO.Id = 0;
            timeRecordDTO.DailyRecordId = dailyRecordId;
            timeRecordDTO.WorkScheduleId = schedule.Id;
            timeRecordDTO.Date = serverDate;
            timeRecordDTO.Time = TimeOnly.FromDateTime(serverNow);

            await _timeRecordService.Create(timeRecordDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = timeRecordDTO;
            _response.Message = "Marcação de ponto cadastrada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível cadastrar a marcação de ponto";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TimeRecordDTO timeRecordDTO)
    {
        if (timeRecordDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        if (!GeoUtils.IsValidGeolocation(timeRecordDTO.Latitude, timeRecordDTO.Longitude))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Formato das coordenadas de geolocalização incorreto";

            return BadRequest(_response);
        }

        try
        {
            var existingTimeRecordDTO = await _timeRecordService.GetById(id);
            if (existingTimeRecordDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A marcação de ponto informada não existe";
                return NotFound(_response);
            }

            await _timeRecordService.Update(timeRecordDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = timeRecordDTO;
            _response.Message = "Marcação de ponto atualizada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar atualizar a marcação de ponto";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingTimeRecordDTO = await _timeRecordService.GetById(id);
            if (existingTimeRecordDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A marcação de ponto informada não existe";
                return NotFound(_response);
            }

            await _timeRecordService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Marcação de ponto removida com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover a marcação de ponto";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
}