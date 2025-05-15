using PontoAll.WebAPI.Objects.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("markpoint")]
public class MarkPoint
{
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("time")]
    public TimeOnly Time { get; set; }

    [Column("location")]
    public Geolocation Location { get; set; }

    [Column("photo")]
    public string Photo {  get; set; }

    [Column("userid")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public MarkPoint() { }

    public MarkPoint(int id, DateOnly date, TimeOnly time, Geolocation location, string photo, int userId)
    {
        Id = id;
        Date = date;
        Time = time;
        Location = location;
        Photo = photo;
        UserId = userId;
    }

}