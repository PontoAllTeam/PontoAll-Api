using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Interfaces;
using PontoAll.WebAPI.Services.Utils;
using System.Text.RegularExpressions;

namespace PontoAll.WebAPI.Services.Entities;

public class CompanyService : GenericService<Company, CompanyDTO>, ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _companyRepository = repository;
        _mapper = mapper;
    }

    public async Task CreateValidatedAsync(CompanyDTO dto)
    {
        NormalizeCompanyFields(dto); 

        CompanyValidator.Validate(dto);

        var existing = await _companyRepository.GetByCNPJ(dto.Cnpj);
        if (existing != null)
            throw new Exception("CNPJ já cadastrado.");

        await Create(dto);
    }

    public async Task UpdateValidatedAsync(CompanyDTO dto, int id)
    {
        NormalizeCompanyFields(dto);

        CompanyValidator.Validate(dto);

        var existing = await _companyRepository.GetByCNPJ(dto.Cnpj);
        if (existing != null && existing.Id != id)
            throw new Exception("CNPJ já cadastrado por outra empresa.");

        await Update(dto, id);
    }

    private void NormalizeCompanyFields(CompanyDTO dto)
    {
        dto.Cep = Regex.Replace(dto.Cep ?? "", @"[^\d]", "");
        dto.Cnpj = Regex.Replace(dto.Cnpj ?? "", @"[^\d]", "");
        dto.BusinessPhone = Regex.Replace(dto.BusinessPhone ?? "", @"[^\d]", "");
    }
}
