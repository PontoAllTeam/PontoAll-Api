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

    [Column("point1")]
    public Geolocation Point1 { get; set; }

    [Column("point2")]
    public Geolocation Point2 { get; set; }

    [Column("point3")]
    public Geolocation Point3 { get; set; }

    [Column("point4")]
    public Geolocation? Point4 { get; set; }

    [Column("point5")]
    public Geolocation? Point5 { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public Geofence() { }

    public Geofence(int id, string name, Geolocation point1, Geolocation point2, Geolocation point3, Geolocation? point4, Geolocation? point5, int companyId)
    {
        Id = id;
        Name = name;
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
        Point4 = point4;
        Point5 = point5;
        CompanyId = companyId;
    }
}