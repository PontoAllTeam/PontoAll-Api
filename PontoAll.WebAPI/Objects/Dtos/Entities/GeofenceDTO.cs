namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class GeofenceDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double RadiusInMeters { get; set; }
    public double CenterLatitude { get; set; }
    public double CenterLongitude { get; set; }
    public int CompanyId { get; set; }
}