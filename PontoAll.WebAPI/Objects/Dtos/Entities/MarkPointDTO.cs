using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class MarkPointDTO
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public Geolocation Location { get; set; }
    public string Photo { get; set; }
    public int UserId { get; set; }
}
