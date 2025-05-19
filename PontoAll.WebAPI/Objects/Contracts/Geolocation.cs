namespace PontoAll.WebAPI.Objects.Contracts;

public class Geolocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Geolocation() {}

    public Geolocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}