namespace PontoAll.WebAPI.Services.Utils;

public static class CityValidator
{
    public static void Validate(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Cidade é obrigatória.");
    }
}
