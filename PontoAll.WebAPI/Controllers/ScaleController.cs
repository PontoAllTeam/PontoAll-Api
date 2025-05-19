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
а а private readonly IScaleService _scaleService;
а а private readonly Response _response;

а а public ScaleController(IScaleService scaleService)
а а {
а а а а _scaleService = scaleService;
а а а а _response = new Response();
а а }

а а [HttpGet]
а а public async Task<IActionResult> GetAll()
а а {
а а а а var scalesDTO = await _scaleService.GetAll();

а а а а _response.Code = ResponseEnum.SUCCESS;
а а а а _response.Data = scalesDTO;
а а а а _response.Message = "Escalas listadas com sucesso";

а а а а return Ok(_response);
а а }

а а [HttpGet("{id}")]
а а public async Task<IActionResult> GetById(int id)
а а {
а а а а var scaleDTO = await _scaleService.GetById(id);

а а а а if (scaleDTO is null)
а а а а {
а а а а а а _response.Code = ResponseEnum.NOT_FOUND;
а а а а а а _response.Data = null;
а а а а а а _response.Message = "Escala nуo encontrada";

а а а а а а return NotFound(_response);
а а а а }

а а а а _response.Code = ResponseEnum.SUCCESS;
а а а а _response.Data = scaleDTO;
а а а а _response.Message = "Escala listada com sucesso";

а а а а return Ok(_response);
а а }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ScaleDTO scaleDTO)
    {
        if (scaleDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados invсlidos";
            return BadRequest(_response);
        }

        try
        {
            // Verifica se o ID jс estс cadastrado no banco
            var existingScale = await _scaleService.GetById(scaleDTO.Id);
            if (existingScale != null)
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "ID jс cadastrado.";
                return BadRequest(_response); // Retorna erro 400 se o ID jс existir
            }

            ScaleValidator.Validate(scaleDTO); // Validaчуo dos dados da escala

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
            _response.Message = "Dados invсlidos";
            return BadRequest(_response);
        }

        try
        {
            var existingScaleDTO = await _scaleService.GetById(id);
            if (existingScaleDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A escala informada nуo existe";
                return NotFound(_response);
            }

            // Verifica se o ID da escala a ser atualizado jс existe (nуo deve existir em outro lugar no banco)
            if (id != scaleDTO.Id)
            {
                var existingScaleById = await _scaleService.GetById(scaleDTO.Id);
                if (existingScaleById != null)
                {
                    _response.Code = ResponseEnum.INVALID;
                    _response.Data = null;
                    _response.Message = "ID jс cadastrado.";
                    return BadRequest(_response); // Retorna erro 400 se o ID jс existir em outro registro
                }
            }

            ScaleValidator.Validate(scaleDTO); // Validaчуo dos dados da escala

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
а а public async Task<IActionResult> Delete(int id)
а а {
а а а а try
а а а а {
а а а а а а var existingScaleDTO = await _scaleService.GetById(id);
а а а а а а if (existingScaleDTO is null)
а а а а а а {
а а а а а а а а _response.Code = ResponseEnum.NOT_FOUND;
а а а а а а а а _response.Data = null;
а а а а а а а а _response.Message = "A escala informada nуo existe";
а а а а а а а а return NotFound(_response);
а а а а а а }

а а а а а а await _scaleService.Remove(id);

а а а а а а _response.Code = ResponseEnum.SUCCESS;
а а а а а а _response.Data = null;
а а а а а а _response.Message = "Escala removida com sucesso";

а а а а а а return Ok(_response);
а а а а }
а а а а catch (Exception ex)
а а а а {
а а а а а а _response.Code = ResponseEnum.ERROR;
а а а а а а _response.Message = "Ocorreu um erro ao tentar remover a escala";
а а а а а а _response.Data = new
а а а а а а {
а а а а а а а а ErrorMessage = ex.Message,
а а а а а а а а StackTrace = ex.StackTrace ?? "No stack trace available"
а а а а а а };
а а а а а а return StatusCode(StatusCodes.Status500InternalServerError, _response);
а а а а }
а а }
}