using PontoAll.WebAPI.Objects.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("geofence")]
public class Geofence
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("radiusinmeters")]
    public double RadiusInMeters { get; set; }

    [Column("centerlatitude")]
    public double CenterLatitude { get; set; }

    [Column("centerlongitude")]
    public double CenterLongitude { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public Geofence() { }

    public Geofence(int id, string name, double radiusInMeters, double centerLatitude, double centerLongitude, int companyId)
    {
        Id = id;
        Name = name;
        RadiusInMeters = radiusInMeters;
        CenterLatitude = centerLatitude;
        CenterLongitude = centerLongitude;
        CompanyId = companyId;
    }
}