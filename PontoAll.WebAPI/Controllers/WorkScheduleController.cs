using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WorkScheduleController : Controller
{
    private readonly IWorkScheduleService _workScheduleService;
    private readonly Response _response;

    public WorkScheduleController(IWorkScheduleService workScheduleService)
    {
        _workScheduleService = workScheduleService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var workSchedulesDTO = await _workScheduleService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = workSchedulesDTO;
        _response.Message = "Escalas listadas com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var workScheduleDTO = await _workScheduleService.GetById(id);

        if (workScheduleDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "Escala não encontrada";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = workScheduleDTO;
        _response.Message = "Escala listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(WorkScheduleDTO workScheduleDTO)
    {
        if (workScheduleDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";
        }

        try
        {
            ValidateWorkSchedule(workScheduleDTO);

            await _workScheduleService.Create(workScheduleDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = workScheduleDTO;
            _response.Message = "Escala cadastrada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = ex.Message;
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return BadRequest(_response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, WorkScheduleDTO workScheduleDTO)
    {
        if (workScheduleDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";
        }

        try
        {
            ValidateWorkSchedule(workScheduleDTO);

            var existingWorkScheduleDTO = await _workScheduleService.GetById(id);
            if (existingWorkScheduleDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A escala informada não existe";
                return NotFound(_response);
            }

            if (id != workScheduleDTO.Id)
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "O ID da URL não corresponde ao ID do corpo da requisição.";
                return BadRequest(_response);
            }

            await _workScheduleService.Update(workScheduleDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = workScheduleDTO;
            _response.Message = "Escala atualizada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = ex.Message;
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return BadRequest(_response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingWorkScheduleDTO = await _workScheduleService.GetById(id);
            if (existingWorkScheduleDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A escala informada não existe";
                return NotFound(_response);
            }

            await _workScheduleService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Escala removida com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover a escala";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    private void ValidateWorkSchedule(WorkScheduleDTO workScheduleDTO)
    {
        if (workScheduleDTO.DayOfMonth < 1 || workScheduleDTO.DayOfMonth > 31)
            throw new Exception("Dia inválido.");

        if (!DateValidator.IsValidMonth(workScheduleDTO.YearMonth))
            throw new Exception("Ano/mês inválido.");

        if (workScheduleDTO.DayType < 1 || workScheduleDTO.DayType > 7)
            throw new Exception("Dia da semana inválido.");

        // Validação apenas do formato de cada campo de horário
        var pickProperties = typeof(WorkScheduleDTO).GetProperties()
            .Where(p => p.Name.StartsWith("MarkTime"))
            .OrderBy(p => p.Name)
            .ToList();

        TimeOnly? previous = null;

        foreach (var prop in pickProperties)
        {
            var value = prop.GetValue(workScheduleDTO);

            TimeValidator.ValidateTime(value, prop.Name);

            if (value is string str && TimeOnly.TryParse(str, out var current))
            {
                if (previous.HasValue && current < previous)
                {
                    throw new Exception($"{prop.Name} deve ser maior ou igual ao hor�rio anterior.");
                }
                previous = current;
            }
            else if (value is TimeOnly currentTime)
            {
                if (previous.HasValue && currentTime < previous)
                {
                    throw new Exception($"{prop.Name} deve ser maior ou igual ao hor�rio anterior.");
                }
                previous = currentTime;
            }
        }
    }
}
