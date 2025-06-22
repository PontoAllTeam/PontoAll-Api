using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TimeRecordController : Controller
{
    private readonly ITimeRecordService _timeRecordService;
    private readonly Response _response;

    public TimeRecordController(ITimeRecordService timeRecordService)
    {
        _timeRecordService = timeRecordService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var timeRecordsDTO = await _timeRecordService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = timeRecordsDTO;
        _response.Message = "Marcações de ponto listadas com sucesso";

        return Ok(_response);
    }

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

        if (!GeolocationValidator.IsValidGeolocation(timeRecordDTO.Location))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Formato das coordenadas de geolocalização incorreto";

            return BadRequest(_response);
        }

        try
        {
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

        if (!GeolocationValidator.IsValidGeolocation(timeRecordDTO.Location))
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
