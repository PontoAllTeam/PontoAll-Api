using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class TimeRecordService : GenericService<TimeRecord, TimeRecordDTO>, ITimeRecordService
{
    private readonly ITimeRecordRepository _timeRecordRepository;
    private readonly IDailyRecordService _dailyRecordService;
    private readonly IFaceRecognitionService _faceRecognitionService;
    private readonly IBiometricDataService _biometricDataService;
    private readonly IMapper _mapper;

    public TimeRecordService(ITimeRecordRepository repository, IDailyRecordService dailyRecordService, IFaceRecognitionService faceRecognitionService, IBiometricDataService biometricDataService, IMapper mapper) : base(repository, mapper)
    {
        _timeRecordRepository = repository;
        _dailyRecordService = dailyRecordService;
        _faceRecognitionService = faceRecognitionService;
        _biometricDataService = biometricDataService;
        _mapper = mapper;
    }

    // --- CORREÇÃO AQUI ---
    public async Task<IEnumerable<TimeRecordDTO>> GetByUserId(int userId)
    {
        // Mudamos de .GetAll() para .GetAllAsync()
        // Se o seu repositório usar outro nome (como ListAsync ou GetAsync), avise, mas GetAllAsync é o padrão.
        var allRecords = await _timeRecordRepository.Get();

        // Filtra na memória apenas os que pertencem ao usuário logado
        var userRecords = allRecords.Where(r => r.UserId == userId).ToList();

        // Retorna mapeado para DTO
        return _mapper.Map<IEnumerable<TimeRecordDTO>>(userRecords);
    }
    // ---------------------

    public new async Task Create(TimeRecordDTO timeRecordDTO)
    {
        // Validar reconhecimento facial se foto fornecida
        await ValidateFaceRecognition(timeRecordDTO.UserId, timeRecordDTO.Photo);

        var dailyRecordId = await _dailyRecordService.EnsureDailyRecordExists(
            timeRecordDTO.UserId,
            timeRecordDTO.WorkScheduleId,
            timeRecordDTO.Date);

        timeRecordDTO.DailyRecordId = dailyRecordId;

        await base.Create(timeRecordDTO);
    }

    private async Task ValidateFaceRecognition(int userId, string photoBase64)
    {
        var biometricData = await _biometricDataService.GetByUserId(userId);
        if (biometricData == null)
            throw new InvalidOperationException("Dados biométricos não encontrados para o usuário");

        var currentEncoding = _faceRecognitionService.ExtractFaceEncodingFromBase64(photoBase64);
        var storedEncoding = biometricData.FacialEmbedding.Select(f => (double)f).ToArray();

        if (!_faceRecognitionService.CompareFaces(storedEncoding, currentEncoding))
            throw new UnauthorizedAccessException("Reconhecimento facial falhou");
    }
}
