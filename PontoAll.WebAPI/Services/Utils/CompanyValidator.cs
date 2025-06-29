using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Services.Utils;
public static class CompanyValidator
{
    public static void Validate(CompanyDTO company)
    {
        RequiredFieldsValidator.NotEmpty(company.FantasyName, "Nome fantasia");

        CpfCnpjValidator.IsValidCNPJ(company.Cnpj);
        PhoneValidator.Validate(company.BusinessPhone);
        CepValidator.Validate(company.Cep);
        RequiredFieldsValidator.NotEmpty(company.State, "Estado");
        RequiredFieldsValidator.City(company.City);
        RequiredFieldsValidator.Street(company.Street);
        RequiredFieldsValidator.Neighborhood(company.Neighborhood);
    }
}
