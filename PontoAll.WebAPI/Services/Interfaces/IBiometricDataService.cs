using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Interfaces;

public interface IBiometricDataService : IGenericService<BiometricData, BiometricDataDTO>
{
    Task<BiometricDataDTO?> GetByUserId(int userId);
    Task SaveFacialEncoding(int userId, double[] encoding);
}