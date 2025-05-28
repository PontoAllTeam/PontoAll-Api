namespace PontoAll.WebAPI.Services.Utils;

public static class RequiredFieldsValidator
{
    public static void NotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} é obrigatório.");
    }
}
