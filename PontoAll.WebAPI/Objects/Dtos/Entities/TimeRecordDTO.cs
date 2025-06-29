using PontoAll.WebAPI.Objects.Contracts;
using System.Text.Json.Serialization;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class TimeRecordDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Justification { get; set; }
    public int UserId { get; set; }
    public string Photo { get; set; }
    public int DailyRecordId { get; set; }
    public int WorkScheduleId { get; set; }

    [JsonIgnore]
    public Geolocation Location => new Geolocation(Latitude, Longitude);
}
