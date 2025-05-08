using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SectorController : Controller
{
    private readonly ISectorService _sectorService;
    private readonly Response _response;

    public SectorController(ISectorService sectorService)
    {
        _sectorService = sectorService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sectors = await _sectorService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = sectors;
        _response.Message = "Setores listados com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sector = await _sectorService.GetById(id);

        if (sector is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = sector;
            _response.Message = "Setor não encontrado";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = sector;
        _response.Message = "Setor listado com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SectorDTO sectorDTO)
    {
        if (sectorDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = sectorDTO;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            await _sectorService.Create(sectorDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = sectorDTO;
            _response.Message = "Setor cadastrado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível cadastrar o setor";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SectorDTO sectorDTO)
    {
        if (sectorDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = sectorDTO;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            var existingSector = await _sectorService.GetById(id);
            if (existingSector is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "O setor informado não existe";
                return NotFound(_response);
            }

            await _sectorService.Update(sectorDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = sectorDTO;
            _response.Message = "Setor atualizado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar atualizar os dados do setor";
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
            var existingSector = await _sectorService.GetById(id);
            if (existingSector is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "O setor informado não existe";
                return NotFound(_response);
            }

            await _sectorService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Setor removido com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover um setor";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
}