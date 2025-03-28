using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CompanyController : Controller
{

    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        this._companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAll();
        return Ok(companies);
    }

    [HttpGet("{id}")] 
    public async Task<IActionResult> GetById(int id)
    {
        var companies = await _companyService.GetById(id);
        if (companies == null)
            return NotFound("Empresa não encontrada");
        return Ok(companies);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CompanyDTO companyDTO)
    {
        try
        {
            await _companyService.Create(companyDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir uma nova empresa");
        }
        return Ok(companyDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CompanyDTO companyDTO)
    {
        try
        {
            await _companyService.Update(companyDTO, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados da empresa" + ex.Message);
        }
        return Ok(companyDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _companyService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover uma empresa.");
        }
        return Ok("Empresa removida com sucesso");
    }
}
