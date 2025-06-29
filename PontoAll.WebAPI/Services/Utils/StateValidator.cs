namespace PontoAll.WebAPI.Services.Utils;

public static class StateValidator
{
    public static void Validate(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("Estado é obrigatório.");

        if (state.Length != 2)
            throw new ArgumentException("Estado inválido. Use apenas a sigla UF (ex: SP, RJ).");
    }
}
