using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ScaleController : Controller
{

    private readonly IScaleService _scaleService;

    public ScaleController(IScaleService scaleService)
    {
        _scaleService = scaleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _scaleService.GetAll();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var companies = await _scaleService.GetById(id);
        if (companies == null)
            return NotFound("Escala não encontrada");
        return Ok(companies);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Scale scale)
    {
        try
        {
            await _scaleService.Create(scale);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir uma nova escala");
        }
        return Ok(scale);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Scale scale)
    {
        try
        {
            await _scaleService.Update(scale, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados da escala" + ex.Message);
        }
        return Ok(scale);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _scaleService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover uma escala.");
        }
        return Ok("Escala removida com sucesso");
    }
}
