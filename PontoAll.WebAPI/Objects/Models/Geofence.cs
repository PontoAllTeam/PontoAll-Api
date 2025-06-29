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

    [Column("point1lat")]
    public double Point1Lat { get; set; }

    [Column("point1lon")]
    public double Point1Lon { get; set; }

    [Column("point2lat")]
    public double Point2Lat { get; set; }

    [Column("point2lon")]
    public double Point2Lon { get; set; }

    [Column("point3lat")]
    public double Point3Lat { get; set; }

    [Column("point3lon")]
    public double Point3Lon { get; set; }

    [Column("point4lat")]
    public double? Point4Lat { get; set; }

    [Column("point4lon")]
    public double? Point4Lon { get; set; }

    [Column("point5lat")]
    public double? Point5Lat { get; set; }

    [Column("point5lon")]
    public double? Point5Lon { get; set; }

    [NotMapped]
    public Geolocation Point1 => new(Point1Lat, Point1Lon);

    [NotMapped]
    public Geolocation Point2 => new(Point2Lat, Point2Lon);

    [NotMapped]
    public Geolocation Point3 => new(Point3Lat, Point3Lon);

    [NotMapped]
    public Geolocation? Point4 => (Point4Lat.HasValue && Point4Lon.HasValue)
        ? new Geolocation(Point4Lat.Value, Point4Lon.Value)
        : null;

    [NotMapped]
    public Geolocation? Point5 => (Point5Lat.HasValue && Point5Lon.HasValue)
        ? new Geolocation(Point5Lat.Value, Point5Lon.Value)
        : null;

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public Geofence() { }

    public Geofence(
        int id,
        string name,
        double point1Lat, double point1Lon,
        double point2Lat, double point2Lon,
        double point3Lat, double point3Lon,
        double? point4Lat, double? point4Lon,
        double? point5Lat, double? point5Lon,
        int companyId)
    {
        Id = id;
        Name = name;

        Point1Lat = point1Lat;
        Point1Lon = point1Lon;

        Point2Lat = point2Lat;
        Point2Lon = point2Lon;

        Point3Lat = point3Lat;
        Point3Lon = point3Lon;

        Point4Lat = point4Lat;
        Point4Lon = point4Lon;

        Point5Lat = point5Lat;
        Point5Lon = point5Lon;

        CompanyId = companyId;
    }
}