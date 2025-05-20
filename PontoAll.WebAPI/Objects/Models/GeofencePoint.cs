using PontoAll.WebAPI.Objects.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("geofencepoint")]
public class GeofencePoint
{
    [Column("id")]
    public int Id { get; set; }

    [Column("location")]
    public Geolocation Location { get; set; }

    [Column("order")]
    public int Order { get; set; }

    [Column("geofenceid")]
    public int GeofenceId { get; set; }
    public Geofence Geofence { get; set; } = null!;

    public GeofencePoint() { }

    public GeofencePoint(int id, Geolocation location, int order, int geofenceId)
    {
        Id = id;
        Location = location;
        Order = order;
        GeofenceId = geofenceId;
    }
}