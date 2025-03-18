using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("scale")]
public class Scale
{
    [Column("id")]
    public int Id { get; set; }

    [Column("day")]
    public int Day { get; set; }

    [Column("yearmonth")]
    public string YearMonth { get; set; }

    [Column("daytype")]
    public DayType DayType { get; set; }

    [Column("pick1")]
    public TimeOnly? Pick1 { get; set; }

    [Column("pick2")]
    public TimeOnly? Pick2 { get; set; }

    [Column("pick3")]
    public TimeOnly? Pick3 { get; set; }

    [Column("pick4")]
    public TimeOnly? Pick4 { get; set; }

    [Column("pick5")]
    public TimeOnly? Pick5 { get; set; }

    [Column("pick6")]
    public TimeOnly? Pick6 { get; set; }

    [Column("pick7")]
    public TimeOnly? Pick7 { get; set; }

    [Column("pick8")]
    public TimeOnly? Pick8 { get; set; }

    [Column("pick9")]
    public TimeOnly? Pick9 { get; set; }

    [Column("pick10")]
    public TimeOnly? Pick10 { get; set; }

    public Scale()
    {

    }

    public Scale(int id, int day, string yearMonth, DayType dayType)
    {
        Id = id;
        Day = day;
        YearMonth = yearMonth;
        DayType = dayType;
    }
}