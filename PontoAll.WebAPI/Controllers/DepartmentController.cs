using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        this._departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departmenties = await _departmentService.GetAll();
        return Ok(departmenties);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var departmenties = await _departmentService.GetById(id);
        if (departmenties == null)
            return NotFound("Departamento não encontrada");
        return Ok(departmenties);
    }

    [HttpPost]
    public async Task<IActionResult> Post(DepartmentDTO departmentDTO)
    {
        try
        {
            await _departmentService.Create(departmentDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo departamento");
        }
        return Ok(departmentDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DepartmentDTO departmentDTO)
    {
        try
        {
            await _departmentService.Update(departmentDTO, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do departamento" + ex.Message);
        }
        return Ok(departmentDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover um departamento.");
        }
        return Ok("Departamento removido com sucesso");
    }
}

