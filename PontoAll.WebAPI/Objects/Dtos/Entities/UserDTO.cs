namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string RecoveryEmail { get; set; }
    public string Registration { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
    public int UserStatus { get; set; }
    public int CompanyId { get; set; }
    public int SectorId { get; set; }
    public string[]? Photos { get; set; }
}
