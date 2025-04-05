using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Entities;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SectorController : Controller
{
    private readonly ISectorService _sectorService;
    public SectorController(ISectorService sectorService)
    {
        this._sectorService = sectorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sectors = await _sectorService.GetAll();
        return Ok(sectors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sectors = await _sectorService.GetById(id);
        if (sectors == null)
            return NotFound("Setor não encontrada");
        return Ok(sectors);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SectorDTO sectorDTO)
    {
        try
        {
            await _sectorService.Create(sectorDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo setor");
        }
        return Ok(sectorDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SectorDTO sectorDTO)
    {
        try
        {
            await _sectorService.Update(sectorDTO, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do setor" + ex.Message);
        }
        return Ok(sectorDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _sectorService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover um setor.");
        }
        return Ok("Sector removido com sucesso");
    }
}