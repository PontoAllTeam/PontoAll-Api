using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Services.Utils;

public class GeolocationValidator
{
    public static bool IsValidGeolocation(Geolocation geolocation)
    {
        return IsValidLongitude(geolocation.Longitude)
            && IsValidLatitude(geolocation.Latitude);
    }

    public static bool IsValidLatitude(double latitude)
    {
        return latitude >= -90 && latitude <= 90;
    }

    public static bool IsValidLongitude(double longitude)
    {
        return longitude >= -180 && longitude <= 180;
    }
}
