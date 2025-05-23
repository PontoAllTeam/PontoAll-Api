using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Services.Utils;

public static class PhoneValidator
{
    public static bool IsValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) { return false; }

        int phoneLength = phone.ExtractNumbers().Length;
        return phoneLength > 9 && phoneLength < 12;
    }
}
