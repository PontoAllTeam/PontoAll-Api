namespace PontoAll.WebAPI.Objects.Contracts;

public class TokenSignature
{
    public string Issuer { get; } = "PontoAll API";
    public string Audience { get; } = "PontoAll WebSite and Mobile";
    public string Key { get; } = "PontoAll_Barrament_API_Autentication";
}