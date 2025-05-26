using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Services.Utils;
using System.Text.RegularExpressions;

namespace PontoAll.WebAPI.Objects.Utils;

public static class UserValidator
{
    public static void Validate(UserDTO user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
            throw new Exception("Nome é obrigatório.");

        user.Phone = StringUtils.ExtractNumbers(user.Phone);

        if (!PhoneValidator.IsValidPhone(user.Phone))
            throw new Exception("Telefone inválido.");

        if (!EmailValidator.IsValidEmail(user.Email))
            throw new Exception("Email inválido.");
    }
}