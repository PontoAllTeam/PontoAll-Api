namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class PermissionRequestDTO
{
    public int Id { get; set; }
    public int RequesterId { get; set; }
    public int? ReviewerId { get; set; }
    public string RequestComments { get; set; }
    public string? ReviewComments { get; set; }
    public int PermissionType { get; set; }
    public int PermissionStatus { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ReviewDate { get; set; }
}
