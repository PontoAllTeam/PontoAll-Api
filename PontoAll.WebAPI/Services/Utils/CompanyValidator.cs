using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Utils;

public static class CompanyValidator
{
    public static void Validate(CompanyDTO company)
    {
        RequiredFieldsValidator.NotEmpty(company.FantasyName, "Nome fantasia");

        CpfCnpjValidator.Validate(company);
        PhoneValidator.Validate(company.BusinessPhone);
        CepValidator.Validate(company.Cep);
        StateValidator.Validate(company.State);
        CityValidator.Validate(company.City);
        StreetValidator.Validate(company.Street);
        NeighborhoodValidator.Validate(company.Neighborhood);
    }
}
