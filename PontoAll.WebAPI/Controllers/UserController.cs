using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var users = await _userService.GetById(id);
        if (users == null)
            return NotFound("Usuário não encontrado");
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDTO userDTO)
    {
        try
        {
            await _userService.Create(userDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo usuário");
        }
        return Ok(userDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserDTO userDTO)
    {
        try
        {
            await _userService.Update(userDTO, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do usuário" + ex.Message);
        }
        return Ok(userDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover um usuário.");
        }
        return Ok("Usuário removida com sucesso");
    }
}

