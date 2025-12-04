using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class BiometricDataService : GenericService<BiometricData, BiometricDataDTO>, IBiometricDataService
{
    private readonly IBiometricDataRepository _biometricDataRepository;
    private readonly IMapper _mapper;

    public BiometricDataService(IBiometricDataRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _biometricDataRepository = repository;
        _mapper = mapper;
    }

    public async Task<BiometricDataDTO?> GetByUserId(int userId)
    {
        var biometricData = await _biometricDataRepository.Get();
        var userBiometric = biometricData.FirstOrDefault(b => b.UserId == userId);
        return userBiometric != null ? _mapper.Map<BiometricDataDTO>(userBiometric) : null;
    }

    public async Task SaveFacialEncoding(int userId, double[] encoding)
    {
        var existing = await GetByUserId(userId);
        var floatEncoding = encoding.Select(d => (float)d).ToArray();

        if (existing != null)
        {
            existing.FacialEmbedding = floatEncoding;
            existing.CreatedAt = DateTime.UtcNow;
            await Update(existing, existing.Id);
        }
        else
        {
            var newBiometric = new BiometricDataDTO
            {
                UserId = userId,
                FacialEmbedding = floatEncoding,
                CreatedAt = DateTime.UtcNow
            };
            await Create(newBiometric);
        }
    }

    public async Task SaveAverageFacialEncoding(int userId, double[][] encodings)
    {
        var averageEncoding = CalculateAverageEncoding(encodings);
        await SaveFacialEncoding(userId, averageEncoding);
    }

    private double[] CalculateAverageEncoding(double[][] encodings)
    {
        var length = encodings[0].Length;
        var average = new double[length];
        
        for (int i = 0; i < length; i++)
        {
            average[i] = encodings.Average(enc => enc[i]);
        }
        
        return average;
    }
}