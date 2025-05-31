using System.Text.Json.Serialization;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class ScaleDTO
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string YearMonth { get; set; }
    public int DayType { get; set; }

    public string? Pick1 { get; set; }

    public string? Pick2 { get; set; }

    public string? Pick3 { get; set; }

    public string? Pick4 { get; set; }

    public string? Pick5 { get; set; }

    public string? Pick6 { get; set; }

    public string? Pick7 { get; set; }

    public string? Pick8 { get; set; }

    public string? Pick9 { get; set; }

    public string? Pick10 { get; set; }

    public int UserId { get; set; }
}
