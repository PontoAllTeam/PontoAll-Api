namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class GeofenceDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public double Point1Lat { get; set; }
    public double Point1Lon { get; set; }

    public double Point2Lat { get; set; }
    public double Point2Lon { get; set; }

    public double Point3Lat { get; set; }
    public double Point3Lon { get; set; }

    public double? Point4Lat { get; set; }
    public double? Point4Lon { get; set; }

    public double? Point5Lat { get; set; }
    public double? Point5Lon { get; set; }
}