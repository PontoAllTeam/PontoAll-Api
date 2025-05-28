namespace PontoAll.WebAPI.Services.Utils;

public static class NeighborhoodValidator
{
    public static void Validate(string neighborhood)
    {
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new ArgumentException("Bairro é obrigatório.");
    }
}
