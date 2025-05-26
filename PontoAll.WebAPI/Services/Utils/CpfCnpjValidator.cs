using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Utils;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Utils;

public static class CpfCnpjValidator
{
    public static void Validate(UserDTO user)
    {
        user.Cpf = StringUtils.ExtractNumbers(user.Cpf);

        if (!IsValidCPF(user.Cpf))
        throw new Exception("CPF inválido.");
    }
    private static bool IsValidCPF(string cpf)
    {
        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return false;

        if (new string(cpf[0], 11) == cpf)
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (cpf[i] - '0') * (10 - i);

        int firstDigit = sum % 11;
        firstDigit = firstDigit < 2 ? 0 : 11 - firstDigit;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (cpf[i] - '0') * (11 - i);

        int secondDigit = sum % 11;
        secondDigit = secondDigit < 2 ? 0 : 11 - secondDigit;

        return cpf[9] - '0' == firstDigit && cpf[10] - '0' == secondDigit;
    }
}
