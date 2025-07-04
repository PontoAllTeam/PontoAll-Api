﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GeofenceController : Controller
{
    private readonly IGeofenceService _geofenceService;
    private readonly Response _response;

    public GeofenceController(IGeofenceService geofenceService)
    {
        _geofenceService = geofenceService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var geofencesDTO = await _geofenceService.GetAll();

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = geofencesDTO;
        _response.Message = "Cercas virtuais listadas com sucesso";

        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var geofenceDTO = await _geofenceService.GetById(id);

        if (geofenceDTO is null)
        {
            _response.Code = ResponseEnum.NOT_FOUND;
            _response.Data = null;
            _response.Message = "Cerca virtual não encontrada";

            return NotFound(_response);
        }

        _response.Code = ResponseEnum.SUCCESS;
        _response.Data = geofenceDTO;
        _response.Message = "Cerca virtual listada com sucesso";

        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(GeofenceDTO geofenceDTO)
    {
        if (geofenceDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            ValidateGeofencePoints(geofenceDTO);

            geofenceDTO.Id = 0;
            await _geofenceService.Create(geofenceDTO);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = geofenceDTO;
            _response.Message = "Cerca virtual cadastrada com sucesso";

            return Ok(_response);
        }
        catch (FormatException ex)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = ex.Message;
            _response.Data = null;
            return BadRequest(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Não foi possível cadastrar a cerca virtual";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, GeofenceDTO geofenceDTO)
    {
        if (geofenceDTO is null)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Data = null;
            _response.Message = "Dados inválidos";

            return BadRequest(_response);
        }

        try
        {
            var existingGeofenceDTO = await _geofenceService.GetById(id);
            if (existingGeofenceDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A cerca virtual informada não existe";
                return NotFound(_response);
            }

            ValidateGeofencePoints(geofenceDTO);

            await _geofenceService.Update(geofenceDTO, id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = geofenceDTO;
            _response.Message = "Cerca virtual atualizada com sucesso";

            return Ok(_response);
        }
        catch (FormatException ex)
        {
            _response.Code = ResponseEnum.INVALID;
            _response.Message = ex.Message;
            _response.Data = null;
            return BadRequest(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar atualizar a cerca virtual";
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
            var existingGeofenceDTO = await _geofenceService.GetById(id);
            if (existingGeofenceDTO is null)
            {
                _response.Code = ResponseEnum.NOT_FOUND;
                _response.Data = null;
                _response.Message = "A cerca virtual informada não existe";
                return NotFound(_response);
            }

            await _geofenceService.Remove(id);

            _response.Code = ResponseEnum.SUCCESS;
            _response.Data = null;
            _response.Message = "Cerca virtual removida com sucesso";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.ERROR;
            _response.Message = "Ocorreu um erro ao tentar remover a cerca virtual";
            _response.Data = new
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace ?? "No stack trace available"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    private static void ValidateGeofencePoints(GeofenceDTO geofenceDTO)
    {
        var geofencePoints = new List<Geolocation>
        {
            new(geofenceDTO.Point1Lat, geofenceDTO.Point1Lon),
            new(geofenceDTO.Point2Lat, geofenceDTO.Point2Lon),
            new(geofenceDTO.Point3Lat, geofenceDTO.Point3Lon)
        };

        if (geofenceDTO.Point4Lat.HasValue && geofenceDTO.Point4Lon.HasValue)
            geofencePoints.Add(new Geolocation(geofenceDTO.Point4Lat.Value, geofenceDTO.Point4Lon.Value));

        if (geofenceDTO.Point5Lat.HasValue && geofenceDTO.Point5Lon.HasValue)
            geofencePoints.Add(new Geolocation(geofenceDTO.Point5Lat.Value, geofenceDTO.Point5Lon.Value));

        for(int i = 0; i < geofencePoints.Count; i++)
        {
            var location = geofencePoints[i];

            if (!GeolocationValidator.IsValidGeolocation(location))
            {
                throw new FormatException($"Formato da coordenada do ponto {i + 1} incorreto");
            }
        }
    }
}
