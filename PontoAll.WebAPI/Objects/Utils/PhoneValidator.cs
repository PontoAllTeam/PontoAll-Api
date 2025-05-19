using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Objects.Utils
{
    public static class PhoneValidator
    {
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) { return false; }
            // Remover caracteres não numéricos
            string cleanPhone = StringUtils.ExtractNumbers(phone);

            // Verifica se o número de caracteres é válido para um número de telefone brasileiro (9 a 11 dígitos)
            int phoneLength = cleanPhone.Length;
            return phoneLength >= 10 && phoneLength <= 11;
        }
    }
}
