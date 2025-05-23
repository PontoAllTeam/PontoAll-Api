using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Utils;
using System.Text.RegularExpressions;

namespace PontoAll.WebAPI.Services.Utils;

public static class CompanyValidator
{
    public static void Validate(CompanyDTO company)
    {
        if (string.IsNullOrWhiteSpace(company.FantasyName))
            throw new ArgumentException("Nome fantasia é obrigatório.");

        if (!IsValidCNPJ(company.Cnpj))
            throw new ArgumentException("CNPJ inválido.");

        if (string.IsNullOrWhiteSpace(company.BusinessPhone))
            throw new ArgumentException("Telefone comercial é obrigatório.");

        if (!PhoneValidator.IsValidPhone(company.BusinessPhone))
            throw new ArgumentException("Telefone comercial inválido.");

        if (!IsValidCEP(company.Cep))
            throw new ArgumentException("CEP inválido.");

        if (!IsValidState(company.State))
            throw new ArgumentException("Estado inválido (use UF).");

        // ➕ Novas validações
        if (string.IsNullOrWhiteSpace(company.City))
            throw new ArgumentException("Cidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(company.Street))
            throw new ArgumentException("Rua é obrigatória.");

        if (string.IsNullOrWhiteSpace(company.Neighborhood))
            throw new ArgumentException("Bairro é obrigatório.");
    }


    public static bool IsValidCNPJ(string cnpj)
    {
        cnpj = StringUtils.ExtractNumbers(cnpj);
        return cnpj.Length == 14 && long.TryParse(cnpj, out _);
    }

    private static bool IsValidCEP(string cep)
    {
        return Regex.IsMatch(cep, @"^\d{5}-?\d{3}$");
    }

    private static bool IsValidState(string state)
    {
        return !string.IsNullOrWhiteSpace(state) && state.Length == 2;
    }
}