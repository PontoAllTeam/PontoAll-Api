using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using System.Threading;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        this._userService = userService;
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
    public async Task<IActionResult> Post(User user)
    {
        try
        {
            await _userService.Create(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo usuário");
        }
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, User user)
    {
        try
        {
            await _userService.Update(user, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do usuário" + ex.Message);
        }
        return Ok(user);
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
        return Ok("Usuário removida com suceso");
    }
}

