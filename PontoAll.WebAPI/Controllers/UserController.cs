using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly Response _response;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usersDTO = await _userService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = usersDTO;
        _response.Message = "Usuários listados com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userDTO = await _userService.GetById(id);

        if (userDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "Usuário não encontrado";

            return NotFound(_response);
        }

        userDTO.Password = "";
        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = userDTO;
        _response.Message = "Usuário listado com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDTO userDTO)
    {
        if (userDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        // Validação de CPF ou CNPJ
        var cpfCnpjNumbers = StringUtils.ExtractNumbers(userDTO.Cpf);
        if (cpfCnpjNumbers.Length == 11)
        {
            if (!CpfCnpjValidator.IsValidCPF(userDTO.Cpf))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "CPF inválido";
                return BadRequest(_response);
            }
        }
        else if (cpfCnpjNumbers.Length == 14)
        {
            if (!CpfCnpjValidator.IsValidCNPJ(userDTO.Cpf))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "CNPJ inválido";
                return BadRequest(_response);
            }
        }
        else
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "CPF ou CNPJ inválido";
            return BadRequest(_response);
        }

        // Validação de e-mail e telefone com mensagens específicas
        try
        {
            EmailValidator.IsValidEmail(userDTO.Email);
            PhoneValidator.Validate(userDTO.Phone);
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = ex.Message;
            return BadRequest(_response);
        }

        var usersDTO = await _userService.GetAll();
        if (CheckDuplicates(usersDTO, userDTO))
        {
            _response.Code = ResponseEnum.CONFLICT;
            _response.Data = null;
            _response.Message = "Este e-mail já está em uso";
            return BadRequest(_response);
        }

        userDTO.Id = 0;
        userDTO.Password = StringUtils.HashString(userDTO.Password);

        await _userService.Create(userDTO);

        userDTO.Password = "";
        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = userDTO;
        _response.Message = "Usuário cadastrado com sucesso";

        return Ok(_response);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] Login login)
    {
        if (login is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            EmailValidator.IsValidEmail(login.Email);
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = ex.Message;
            return BadRequest(_response);
        }

        try
        {
            login.Password = StringUtils.HashString(login.Password);
            var userDTO = await _userService.Login(login);

            if (userDTO is null)
            {
                login.Password = "";
                _response.Code = ResponseEnum.INVALID;
                _response.Data = login;
                _response.Message = "Email ou senha incorretos";

                return BadRequest(_response);
            }

            var token = _tokenService.GenerateToken(userDTO);
            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = token;
            _response.Message = "Login realizado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível realizar o login";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost("Validate")]
    [AllowAnonymous]
    public async Task<ActionResult> Validate([FromBody] string token)
    {
        if (token is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            var email = _tokenService.ExtractSubjectEmail(token);

            if (string.IsNullOrEmpty(email) || await _userService.GetByEmail(email) == null)
            {
                _response.Code = ResponseEnum.UNAUTHORIZED;
                _response.Data = null;
                _response.Message = "Token inválido";

                return Unauthorized(_response);
            }
            else if (!await _tokenService.ValidateToken(token))
            {
                _response.Code = ResponseEnum.UNAUTHORIZED;
                _response.Data = null;
                _response.Message = "Token inválido";

                return Unauthorized(_response);
            }

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = token;
            _response.Message = "Token validado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível validar o token";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserDTO userDTO)
    {
        if (userDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        var existingUserDTO = await _userService.GetById(id);
        if (existingUserDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "O usuário informado não existe";
            return NotFound(_response);
        }

        var cpfCnpjNumbers = StringUtils.ExtractNumbers(userDTO.Cpf);
        if (cpfCnpjNumbers.Length == 11)
        {
            if (!CpfCnpjValidator.IsValidCPF(userDTO.Cpf))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "CPF inválido";
                return BadRequest(_response);
            }
        }
        else if (cpfCnpjNumbers.Length == 14)
        {
            if (!CpfCnpjValidator.IsValidCNPJ(userDTO.Cpf))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Data = null;
                _response.Message = "CNPJ inválido";
                return BadRequest(_response);
            }
        }
        else
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "CPF ou CNPJ inválido";
            return BadRequest(_response);
        }

        try
        {
            EmailValidator.IsValidEmail(userDTO.Email);
            PhoneValidator.Validate(userDTO.Phone);
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = ex.Message;
            return BadRequest(_response);
        }

        var usersDTO = await _userService.GetAll();
        if (CheckDuplicates(usersDTO, userDTO))
        {
            userDTO.Password = "";
            _response.Code = ResponseEnum.CONFLICT;
            _response.Data = userDTO;
            _response.Message = "Este e-mail já está em uso";
            return BadRequest(_response);
        }

        await _userService.Update(userDTO, id);

        userDTO.Password = "";
        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = userDTO;
        _response.Message = "Usuário atualizado com sucesso";

        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingUserDTO = await _userService.GetById(id);
            if (existingUserDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "O usuário informado não existe";
                return NotFound(_response);
            }

            await _userService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Usuário removido com sucesso";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover o usuário";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    // ===== Métodos auxiliares para validações internas =====

    private bool CheckDuplicates(IEnumerable<UserDTO> usersDTO, UserDTO userDTO)
    {
        return usersDTO.Any(u => u.Email.Equals(userDTO.Email, StringComparison.OrdinalIgnoreCase)
            && u.Id != userDTO.Id);
    }
}
