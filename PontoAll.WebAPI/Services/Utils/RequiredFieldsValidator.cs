namespace PontoAll.WebAPI.Services.Utils;

public static class RequiredFieldsValidator
{
    public static void NotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} é obrigatório.");
    }

    public static void Street(string? street)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Rua é obrigatória.");
    }

    public static void Neighborhood(string? neighborhood)
    {
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new ArgumentException("Bairro é obrigatório.");
    }

    public static void City(string? city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Cidade é obrigatória.");
    }
}
