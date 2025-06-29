using AutoMapper;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Objects.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkScheduleDTO, WorkSchedule>()
            .ForMember(dest => dest.MarkTime1, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime1)))
            .ForMember(dest => dest.MarkTime2, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime2)))
            .ForMember(dest => dest.MarkTime3, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime3)))
            .ForMember(dest => dest.MarkTime4, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime4)))
            .ForMember(dest => dest.MarkTime5, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime5)))
            .ForMember(dest => dest.MarkTime6, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime6)))
            .ForMember(dest => dest.MarkTime7, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime7)))
            .ForMember(dest => dest.MarkTime8, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime8)))
            .ForMember(dest => dest.MarkTime9, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime9)))
            .ForMember(dest => dest.MarkTime10, opt => opt.MapFrom(src => ParseTimeOnly(src.MarkTime10)));

        CreateMap<WorkSchedule, WorkScheduleDTO>()
            .ForMember(dest => dest.MarkTime1, opt => opt.MapFrom(src => src.MarkTime1.HasValue ? src.MarkTime1.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime2, opt => opt.MapFrom(src => src.MarkTime2.HasValue ? src.MarkTime2.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime3, opt => opt.MapFrom(src => src.MarkTime3.HasValue ? src.MarkTime3.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime4, opt => opt.MapFrom(src => src.MarkTime4.HasValue ? src.MarkTime4.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime5, opt => opt.MapFrom(src => src.MarkTime5.HasValue ? src.MarkTime5.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime6, opt => opt.MapFrom(src => src.MarkTime6.HasValue ? src.MarkTime6.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime7, opt => opt.MapFrom(src => src.MarkTime7.HasValue ? src.MarkTime7.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime8, opt => opt.MapFrom(src => src.MarkTime8.HasValue ? src.MarkTime8.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime9, opt => opt.MapFrom(src => src.MarkTime9.HasValue ? src.MarkTime9.Value.ToString("HH:mm:ss") : null))
            .ForMember(dest => dest.MarkTime10, opt => opt.MapFrom(src => src.MarkTime10.HasValue ? src.MarkTime10.Value.ToString("HH:mm:ss") : null));

        CreateMap<CompanyDTO, Company>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<DepartmentDTO, Department>().ReverseMap();
        CreateMap<SectorDTO, Sector>().ReverseMap();
        CreateMap<TimeRecordDTO, TimeRecord>().ReverseMap();
        CreateMap<SubscriptionPlanDTO, SubscriptionPlan>().ReverseMap();
        CreateMap<PermissionRequestDTO, PermissionRequest>().ReverseMap();
        CreateMap<BiometricDataDTO, BiometricData>().ReverseMap();
        CreateMap<DailyRecordDTO, DailyRecord>().ReverseMap();
        CreateMap<GeofenceDTO, Geofence>().ReverseMap();
    }

    private static TimeOnly? ParseTimeOnly(string? timeString)
    {
        if (string.IsNullOrWhiteSpace(timeString))
            return null;

        if (TimeOnly.TryParse(timeString, out var time))
            return time;

        throw new Exception($"Formato inválido para horário: {timeString}");
    }
}
