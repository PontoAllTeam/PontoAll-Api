using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Enums;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DailyRecordController : Controller
{
    private readonly IDailyRecordService _dailyRecordService;
    private readonly IUserService _userService;
    private readonly Response _response;

    public DailyRecordController(IDailyRecordService dailyRecordService, IUserService userService)
    {
        _dailyRecordService = dailyRecordService;
        _userService = userService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dailyRecordsDTO = await _dailyRecordService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = dailyRecordsDTO;
        _response.Message = "Resumos diários listados com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dailyRecordDTO = await _dailyRecordService.GetById(id);

        if (dailyRecordDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Message = "Resumo diário não encontrado";
            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = dailyRecordDTO;
        _response.Message = "Resumo diário listado com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(DailyRecordDTO dailyRecordDTO)
    {
        if (dailyRecordDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        try
        {
            if (!await _userService.IsUserActive(dailyRecordDTO.EmployeeId))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Message = "Funcionário inativo ou não encontrado";
                return BadRequest(_response);
            }

            if (dailyRecordDTO.ReviewerId.HasValue && !await _userService.IsUserActive(dailyRecordDTO.ReviewerId.Value))
            {
                _response.Code = ResponseEnum.INVALID;
                _response.Message = "Revisor inativo ou não encontrado";
                return BadRequest(_response);
            }

            dailyRecordDTO.Id = 0;
            await _dailyRecordService.Create(dailyRecordDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = dailyRecordDTO;
            _response.Message = "Resumo diário cadastrado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao cadastrar resumo diário";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DailyRecordDTO dailyRecordDTO)
    {
        if (dailyRecordDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = "Dados inválidos";
            return BadRequest(_response);
        }

        try
        {
            var existingDailyRecordDTO = await _dailyRecordService.GetById(id);
            if (existingDailyRecordDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Message = "O resumo diário informado não existe";
                return NotFound(_response);
            }

            await _dailyRecordService.Update(dailyRecordDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = dailyRecordDTO;
            _response.Message = "Resumo diário atualizado com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao atualizar resumo diário";
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
            var existingDailyRecordDTO = await _dailyRecordService.GetById(id);
            if (existingDailyRecordDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Message = "O resumo diário informado não existe";
                return NotFound(_response);
            }

            await _dailyRecordService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Message = "Resumo diário removido com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Erro ao remover resumo diário";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
}