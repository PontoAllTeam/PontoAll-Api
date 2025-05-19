using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Objects.Utils
{
    public static class PhoneValidator
    {
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) { return false; }
            // Remover caracteres n�o num�ricos
            string cleanPhone = StringUtils.ExtractNumbers(phone);

            // Verifica se o n�mero de caracteres � v�lido para um n�mero de telefone brasileiro (9 a 11 d�gitos)
            int phoneLength = cleanPhone.Length;
            return phoneLength >= 10 && phoneLength <= 11;
        }
    }
}
