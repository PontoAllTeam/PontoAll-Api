using AutoMapper;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Objects.Dtos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ScaleDTO, Scale>()
                .ForMember(dest => dest.Pick1, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick1)))
                .ForMember(dest => dest.Pick2, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick2)))
                .ForMember(dest => dest.Pick3, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick3)))
                .ForMember(dest => dest.Pick4, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick4)))
                .ForMember(dest => dest.Pick5, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick5)))
                .ForMember(dest => dest.Pick6, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick6)))
                .ForMember(dest => dest.Pick7, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick7)))
                .ForMember(dest => dest.Pick8, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick8)))
                .ForMember(dest => dest.Pick9, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick9)))
                .ForMember(dest => dest.Pick10, opt => opt.MapFrom(src => ParseTimeOnly(src.Pick10)));

            CreateMap<Scale, ScaleDTO>()
                .ForMember(dest => dest.Pick1, opt => opt.MapFrom(src => src.Pick1.HasValue ? src.Pick1.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick2, opt => opt.MapFrom(src => src.Pick2.HasValue ? src.Pick2.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick3, opt => opt.MapFrom(src => src.Pick3.HasValue ? src.Pick3.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick4, opt => opt.MapFrom(src => src.Pick4.HasValue ? src.Pick4.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick5, opt => opt.MapFrom(src => src.Pick5.HasValue ? src.Pick5.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick6, opt => opt.MapFrom(src => src.Pick6.HasValue ? src.Pick6.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick7, opt => opt.MapFrom(src => src.Pick7.HasValue ? src.Pick7.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick8, opt => opt.MapFrom(src => src.Pick8.HasValue ? src.Pick8.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick9, opt => opt.MapFrom(src => src.Pick9.HasValue ? src.Pick9.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.Pick10, opt => opt.MapFrom(src => src.Pick10.HasValue ? src.Pick10.Value.ToString("HH:mm:ss") : null));

            CreateMap<CompanyDTO, Company>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<DepartmentDTO, Department>().ReverseMap();
            CreateMap<SectorDTO, Sector>().ReverseMap();
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
}
