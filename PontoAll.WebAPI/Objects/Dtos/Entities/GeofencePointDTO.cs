using PontoAll.WebAPI.Objects.Contracts;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class GeofencePointDTO
{
    public int Id { get; set; }
    public Geolocation Location { get; set; }
    public int Order { get; set; }
    public int GeofenceId { get; set; }
}
