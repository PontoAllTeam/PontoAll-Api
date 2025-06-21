using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class GeofenceDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Geolocation Point1 { get; set; }
    public Geolocation Point2 { get; set; }
    public Geolocation Point3 { get; set; }
    public Geolocation? Point4 { get; set; }
    public Geolocation? Point5 { get; set; }
    public int CompanyId { get; set; }
}