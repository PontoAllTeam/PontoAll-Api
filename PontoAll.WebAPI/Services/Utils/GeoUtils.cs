namespace PontoAll.WebAPI.Services.Utils;

public class GeoUtils
{
    // Haversine (metros)
    public static double DistanceMeters(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000; // raio da Terra (m)
        double dLat = ToRad(lat2 - lat1);
        double dLon = ToRad(lon2 - lon1);
        double a = Math.Sin(dLat/2)*Math.Sin(dLat/2) +
                   Math.Cos(ToRad(lat1))*Math.Cos(ToRad(lat2)) *
                   Math.Sin(dLon/2)*Math.Sin(dLon/2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
        return R * c;
    }

    private static double ToRad(double deg) => deg * Math.PI / 180.0;

    public static bool IsValidLatitude(double latitude)
    {
        // A latitude deve ser maior ou igual a -90 e menor ou igual a 90.
        return latitude >= -90.0 && latitude <= 90.0;
    }

    public static bool IsValidLongitude(double longitude)
    {
        // A longitude deve ser maior ou igual a -180 e menor ou igual a 180.
        return longitude >= -180.0 && longitude <= 180.0;
    }
}
