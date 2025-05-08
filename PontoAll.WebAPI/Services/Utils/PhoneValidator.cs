namespace PontoAll.WebAPI.Services.Utils;

public class PhoneValidator
{
    public static bool IsValidPhone(string phone)
    {
        int phoneLength = phone.ExtractNumbers().Length;
        return phoneLength > 9 && phoneLength < 12;
    }
}