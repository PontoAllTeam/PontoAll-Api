namespace PontoAll.WebAPI.Services.Utils;

public static class StreetValidator
{
    public static void Validate(string street)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Rua é obrigatória.");
    }
}
