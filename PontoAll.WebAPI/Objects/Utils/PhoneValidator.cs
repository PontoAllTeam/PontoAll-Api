namespace PontoAll.WebAPI.Objects.Utils;

public class PhoneValidator
{
    public static bool IsValidPhone(string phone)
    {
        int phoneLength = StringUtils.ExtractNumbers(phone).Length;
        return phoneLength > 9 && phoneLength < 12;
    }
}