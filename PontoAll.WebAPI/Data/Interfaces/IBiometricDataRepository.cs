using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Interfaces;

public interface IBiometricDataRepository : IGenericRepository<BiometricData>
{
    Task<BiometricData?> GetByUserId(int userId);
}