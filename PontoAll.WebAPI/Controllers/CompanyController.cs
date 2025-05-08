using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Utils;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CompanyController : Controller
{
    private readonly ICompanyService _companyService;
    private readonly Response _response;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = companies;
        _response.Message = "Empresas listadas com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var company = await _companyService.GetById(id);

        if (company is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = company;
            _response.Message = "Empresa não encontrada";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = company;
        _response.Message = "Empresa listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CompanyDTO companyDTO)
    {
        if (companyDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = companyDTO;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        if (!CheckCompanyInfo(companyDTO))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = companyDTO;
            _response.Message = "Formato incorreto de email ou telefone";

            return BadRequest(_response);
        }

        try
        {
            await _companyService.Create(companyDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = companyDTO;
            _response.Message = "Empresa cadastrada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível cadastrar a empresa";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CompanyDTO companyDTO)
    {
        if (companyDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = companyDTO;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        if (!CheckCompanyInfo(companyDTO))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = companyDTO;
            _response.Message = "Formato incorreto de email ou telefone";

            return BadRequest(_response);
        }

        try
        {
            var existingCompany = await _companyService.GetById(id);
            if (existingCompany is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A empresa informada não existe";
                return NotFound(_response);
            }

            await _companyService.Update(companyDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = companyDTO;
            _response.Message = "Empresa atualizada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar atualizar os dados da empresa";
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
            var existingCompany = await _companyService.GetById(id);
            if (existingCompany is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A empresa informada não existe";
                return NotFound(_response);
            }

            await _companyService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Empresa removida com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover a empresa";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    private static bool CheckCompanyInfo(CompanyDTO companyDTO)
    {
        bool isValidPhone = PhoneValidator.IsValidPhone(companyDTO.BusinessPhone);
        bool isValidEmail = EmailValidator.IsValidEmail(companyDTO.Email);

        return isValidPhone && isValidEmail;
    }
}