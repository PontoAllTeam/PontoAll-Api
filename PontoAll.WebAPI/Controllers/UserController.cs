using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Utils;
using PontoAll.WebAPI.Objects.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        if (users is null)
            return NotFound("Usuário não encontrado");
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDTO userDTO)
    {
        if (userDTO is null)
        {
            return BadRequest("Dados inválidos");
        } 
        
        try
        {
            if (!CheckUserInfo(userDTO))
            {
                return BadRequest("Formato incorreto de email ou telefone");
            }

            userDTO.Id = 0;
            var usersDTO = await _userService.GetAll();

            if (CheckDuplicates(usersDTO, userDTO))
            {
                return BadRequest("Esse e-mail já está em uso");
            }

            userDTO.Password = StringUtils.HashString(userDTO.Password);
            await _userService.Create(userDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo usuário");
        }

        userDTO.Password = "";
        return Ok(userDTO);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] Login login)
    {
        if (login is null)
        {
            return BadRequest("Dado(s) inválido");
        }

        if (!EmailValidator.IsValidEmail(login.Email))
        {
            return BadRequest("Formato de email incorreto");
        }

        try
        {
            login.Password = StringUtils.HashString(login.Password);
            var userDTO = await _userService.Login(login);

            if (userDTO is null)
            {
                return BadRequest("Email ou senha incorretos");
            }

            var token = new Token();
            token.GenerateToken(userDTO.Email);

            return Ok(token);
        }
        catch (Exception ex)
        {
            var errorData = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorData);
        }
    }

    [HttpPost("Validate")]
    [AllowAnonymous]
    public async Task<ActionResult> Validate([FromBody] Token token)
    {
        if (token is null)
        {
            return BadRequest("Dado inválido");
        }

        try
        {
            var email = token.ExtractSubject();

            if (string.IsNullOrEmpty(email) || await _userService.GetByEmail(email) == null)
            {
                return Unauthorized("Token inválido");
            }
            else if (!token.ValidateToken())
            {
                return Unauthorized("Token inválido");
            }

            return Ok(token);
        }
        catch (Exception ex)
        {
            var errorData = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorData);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserDTO userDTO)
    {
        if (userDTO is null)
        {
            return BadRequest("Dados inválidos");
        }

        try
        {
            var existingUserDTO = await _userService.GetById(userDTO.Id);
            if (existingUserDTO is null)
            {
                return NotFound("O usuário informado não existe!");
            }

            if (!CheckUserInfo(userDTO))
            {
                return BadRequest("Formato incorreto de email ou telefone");
            }

            var usersDTO = await _userService.GetAll();

            if (CheckDuplicates(usersDTO, userDTO))
            {
                return BadRequest("Esse e-mail já está em uso");
            }

            // userDTO.Password = StringUtils.HashString(userDTO.Password);
            await _userService.Update(userDTO, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar os dados do usuário" + ex.Message);
        }

        userDTO.Password = "";
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
        return Ok("Usuário removido com sucesso");
    }

    private static bool CheckUserInfo(UserDTO userDTO)
    {
        bool isValidPhone = PhoneValidator.IsValidPhone(userDTO.Phone);
        bool isValidEmail = EmailValidator.IsValidEmail(userDTO.Email);
        bool isValidRecoveryEmail = EmailValidator.IsValidEmail(userDTO.RecoveryEmail);

        return isValidPhone && isValidEmail && isValidRecoveryEmail;
    }

    private static bool CheckDuplicates(IEnumerable<UserDTO> usersDTO, UserDTO userDTO)
	{
        foreach (var user in usersDTO)
        {
            if (userDTO.Id == user.Id)
		    {
			    continue;
		    }

		    if (StringUtils.CompareString(userDTO.Email, user.Email))
            {
                return true;
            }
        }

        return false;
    }
}

