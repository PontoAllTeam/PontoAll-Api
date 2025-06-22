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
public class MarkPointController : Controller
{
    private readonly IMarkPointService _markPointService;
    private readonly Response _response;

    public MarkPointController(IMarkPointService markPointService)
    {
        _markPointService = markPointService;
        _response = new Response();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var markPointsDTO = await _markPointService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = markPointsDTO;
        _response.Message = "Marcações de ponto listadas com sucesso";

        return Ok(_response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var markPointDTO = await _markPointService.GetById(id);

        if (markPointDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "Marcação de ponto não encontrada";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = markPointDTO;
        _response.Message = "Marcação de ponto listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TimeRecordDTO markPointDTO)
    {
        if (markPointDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        if (!GeolocationValidator.IsValidGeolocation(markPointDTO.Location))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Formato das coordenadas de geolocalização incorreto";

            return BadRequest(_response);
        }

        try
        {
            await _markPointService.Create(markPointDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = markPointDTO;
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
    public async Task<IActionResult> Put(int id, TimeRecordDTO markPointDTO)
    {
        if (markPointDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        if (!GeolocationValidator.IsValidGeolocation(markPointDTO.Location))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Formato das coordenadas de geolocalização incorreto";

            return BadRequest(_response);
        }

        try
        {
            var existingMarkPointDTO = await _markPointService.GetById(id);
            if (existingMarkPointDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A marcação de ponto informada não existe";
                return NotFound(_response);
            }

            await _markPointService.Update(markPointDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = markPointDTO;
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
            var existingMarkPointDTO = await _markPointService.GetById(id);
            if (existingMarkPointDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A marcação de ponto informada não existe";
                return NotFound(_response);
            }

            await _markPointService.Remove(id);

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
