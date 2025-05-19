using System.ComponentModel.DataAnnotations;

namespace PontoAll.WebAPI.Objects.Contracts;

public class Geolocation
{
    [Range(-90, 90, ErrorMessage = "Latitude deve estar entre -90 e 90 graus.")]
    public double Latitude { get; set; }

    [Range(-180, 180, ErrorMessage = "Longitude deve estar entre -180 e 180 graus.")]
    public double Longitude { get; set; }

    public Geolocation() {}

    public Geolocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}