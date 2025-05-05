using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class DepartmentService : GenericService<Department, DepartmentDTO>, IDepartmentService
{
   
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _departmentRepository = repository;
        _mapper = mapper;
    }

}