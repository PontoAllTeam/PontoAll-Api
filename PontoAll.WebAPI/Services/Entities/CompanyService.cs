using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class CompanyService : GenericService<Company>, ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _companyRepository = repository;
        _mapper = mapper;
    }

}