using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("geofence")]
public class Geofence
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public ICollection<GeofencePoint> GeofencePoints { get; } = [];

    public Geofence() { }

    public Geofence(int id, string name, int companyId)
    {
        Id = id;
        Name = name;
        CompanyId = companyId;
    }
}