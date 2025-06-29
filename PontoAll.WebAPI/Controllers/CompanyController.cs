using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Services.Utils;

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
        var companiesDTO = await _companyService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = companiesDTO;
        _response.Message = "Empresas listadas com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var companyDTO = await _companyService.GetById(id);

        if (companyDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Message = "Empresa não encontrada";
            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = companyDTO;
        _response.Message = "Empresa listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CompanyDTO companyDTO)
    {
        if (companyDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        NormalizeCompanyFields(companyDTO);

        if (!CpfCnpjValidator.IsValidCNPJ(companyDTO.Cnpj))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "CNPJ inválido.";
            return BadRequest(_response);
        }

        if (!EmailValidator.IsValidEmail(companyDTO.Email))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Email inválido.";
            return BadRequest(_response);
        }

        if (!PhoneValidator.IsValidPhone(companyDTO.BusinessPhone))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Telefone inválido. Use DDD + número, ex: 11999999999.";
            return BadRequest(_response);
        }

        try
        {
            CompanyValidator.Validate(companyDTO);

            var existing = await _companyService.GetAll();
            if (existing.Any(c => c.Cnpj == companyDTO.Cnpj))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Message = "CNPJ já cadastrado.";
                return BadRequest(_response);
            }

            companyDTO.Id = 0;
            await _companyService.Create(companyDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = companyDTO;
            _response.Message = "Empresa cadastrada com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao cadastrar empresa";
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
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        NormalizeCompanyFields(companyDTO);

        if (!CpfCnpjValidator.IsValidCNPJ(companyDTO.Cnpj))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "CNPJ inválido.";
            return BadRequest(_response);
        }

        if (!EmailValidator.IsValidEmail(companyDTO.Email))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Email inválido.";
            return BadRequest(_response);
        }

        if (!PhoneValidator.IsValidPhone(companyDTO.BusinessPhone))
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Telefone inválido. Use DDD + número, ex: 11999999999.";
            return BadRequest(_response);
        }

        try
        {
            var existingCompanyDTO = await _companyService.GetById(id);
            if (existingCompanyDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Message = "A empresa informada não existe";
                return NotFound(_response);
            }

            CompanyValidator.Validate(companyDTO);

            var allCompanies = await _companyService.GetAll();
            var cnpjDuplicado = allCompanies.Any(c => c.Cnpj == companyDTO.Cnpj && c.Id != id);
            if (cnpjDuplicado)
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Message = "CNPJ já cadastrado por outra empresa.";
                return BadRequest(_response);
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
            _response.Message = "Erro ao atualizar empresa";
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
            var existingCompanyDTO = await _companyService.GetById(id);
            if (existingCompanyDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Message = "A empresa informada não existe";
                return NotFound(_response);
            }

            await _companyService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Message = "Empresa removida com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao remover empresa";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    private static void NormalizeCompanyFields(CompanyDTO dto)
    {
        dto.Cep = StringUtils.ExtractNumbers(dto.Cep);
        dto.Cnpj = StringUtils.ExtractNumbers(dto.Cnpj);
        dto.BusinessPhone = StringUtils.ExtractNumbers(dto.BusinessPhone);
    }
}
