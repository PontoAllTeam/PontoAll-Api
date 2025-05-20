using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Utils;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ScaleController : Controller
{
� � private readonly IScaleService _scaleService;
� � private readonly Response _response;

� � public ScaleController(IScaleService scaleService)
� � {
� � � � _scaleService = scaleService;
� � � � _response = new Response();
� � }

� � [HttpGet]
� � public async Task<IActionResult> GetAll()
� � {
� � � � var scalesDTO = await _scaleService.GetAll();

� � � � _response.Code = ResponseEnum.SUCCESS;
� � � � _response.Data = scalesDTO;
� � � � _response.Message = "Escalas listadas com sucesso";

� � � � return Ok(_response);
� � }

� � [HttpGet("{id}")]
� � public async Task<IActionResult> GetById(int id)
� � {
� � � � var scaleDTO = await _scaleService.GetById(id);

� � � � if (scaleDTO is null)
� � � � {
� � � � � � _response.Code = ResponseEnum.NOT_FOUND;
� � � � � � _response.Data = null;
� � � � � � _response.Message = "Escala n�o encontrada";

� � � � � � return NotFound(_response);
� � � � }

� � � � _response.Code = ResponseEnum.SUCCESS;
� � � � _response.Data = scaleDTO;
� � � � _response.Message = "Escala listada com sucesso";

� � � � return Ok(_response);
� � }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ScaleDTO scaleDTO)
    {
        if (scaleDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inv�lidos";
            return BadRequest(_response);
        }

        try
        {
            // Verifica se o ID j� est� cadastrado no banco
            var existingScale = await _scaleService.GetById(scaleDTO.Id);
            if (existingScale != null)
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "ID j� cadastrado.";
                return BadRequest(_response);
            }

            ScaleValidator.Validate(scaleDTO);

            await _scaleService.Create(scaleDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = scaleDTO;
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
    public async Task<IActionResult> Put(int id, ScaleDTO scaleDTO)
    {
        if (scaleDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inv�lidos";
            return BadRequest(_response);
        }

        try
        {
            var existingScaleDTO = await _scaleService.GetById(id);
            if (existingScaleDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A escala informada n�o existe";
                return NotFound(_response);
            }

            if (id != scaleDTO.Id)
            {
                var existingScaleById = await _scaleService.GetById(scaleDTO.Id);
                if (existingScaleById != null)
                {
                    _response.Code = ResponseEnum.INVALID;
                    _response.Data = null;
                    _response.Message = "ID j� cadastrado.";
                    return BadRequest(_response); 
                }
            }

            ScaleValidator.Validate(scaleDTO);

            await _scaleService.Update(scaleDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = scaleDTO;
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
� � public async Task<IActionResult> Delete(int id)
� � {
� � � � try
� � � � {
� � � � � � var existingScaleDTO = await _scaleService.GetById(id);
� � � � � � if (existingScaleDTO is null)
� � � � � � {
� � � � � � � � _response.Code = ResponseEnum.NOT_FOUND;
� � � � � � � � _response.Data = null;
� � � � � � � � _response.Message = "A escala informada n�o existe";
� � � � � � � � return NotFound(_response);
� � � � � � }

� � � � � � await _scaleService.Remove(id);

� � � � � � _response.Code = ResponseEnum.SUCCESS;
� � � � � � _response.Data = null;
� � � � � � _response.Message = "Escala removida com sucesso";

� � � � � � return Ok(_response);
� � � � }
� � � � catch (Exception ex)
� � � � {
� � � � � � _response.Code = ResponseEnum.ERROR;
� � � � � � _response.Message = "Ocorreu um erro ao tentar remover a escala";
� � � � � � _response.Data = new
� � � � � � {
� � � � � � � � ErrorMessage = ex.Message,
� � � � � � � � StackTrace = ex.StackTrace ?? "No stack trace available"
� � � � � � };
� � � � � � return StatusCode(StatusCodes.Status500InternalServerError, _response);
� � � � }
� � }
}