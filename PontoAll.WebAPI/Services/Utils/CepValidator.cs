using System.Text.RegularExpressions;

namespace PontoAll.WebAPI.Services.Utils;

public static class CepValidator
{
    public static void Validate(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            throw new ArgumentException("CEP é obrigatório.");

        if (!Regex.IsMatch(cep, @"^\d{5}-?\d{3}$"))
            throw new ArgumentException("CEP inválido. Formato esperado: 00000-000 ou 00000000.");
    }
}
