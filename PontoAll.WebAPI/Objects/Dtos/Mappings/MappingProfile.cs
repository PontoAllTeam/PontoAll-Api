using AutoMapper;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Objects.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ScaleDTO, Scale>().ReverseMap();
        CreateMap<CompanyDTO, Company>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<DepartmentDTO, Department>().ReverseMap();
        CreateMap<SectorDTO, Sector>().ReverseMap();
        CreateMap<MarkPointDTO, MarkPoint>().ReverseMap();
    }
}
