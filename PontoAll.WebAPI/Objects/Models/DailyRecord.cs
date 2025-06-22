using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("dailyrecord")]
public class DailyRecord
{
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("totalworkedhours")]
    public double TotalWorkedHours { get; set; }

    [Column("expectedhours")]
    public double ExpectedHours { get; set; }

    [Column("overtimehours")]
    public double OvertimeHours { get; set; }

    [Column("missinghours")]
    public double MissingHours { get; set; }

    [Column("isabsent")]
    public bool IsAbsent { get; set; }

    [Column("reviewstatus")]
    public ReviewStatus ReviewStatus { get; set; }

    [Column("reviewercomments")]
    public string? ReviewerComments { get; set; }

    [Column("reviewedat")]
    public DateTime? ReviewedAt { get; set; }

    [Column("workscheduleid")]
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; } = null!;

    [Column("employeeid")]
    public int EmployeeId { get; set; }
    public User Employee { get; set; } = null!;

    [Column("reviewerid")]
    public int? ReviewerId { get; set; }
    public User Reviewer { get; set; } = null!;

    public DailyRecord() { }

    public DailyRecord(int id, DateOnly date, double totalWorkedHours, double expectedHours, double overtimeHours, double missingHours, bool isAbsent, ReviewStatus reviewStatus, string? reviewerComments, DateTime? reviewedAt, int employeeId, int? reviewerId)
    {
        Id = id;
        Date = date;
        TotalWorkedHours = totalWorkedHours;
        ExpectedHours = expectedHours;
        OvertimeHours = overtimeHours;
        MissingHours = missingHours;
        IsAbsent = isAbsent;
        ReviewStatus = reviewStatus;
        ReviewerComments = reviewerComments;
        ReviewedAt = reviewedAt;
        EmployeeId = employeeId;
        ReviewerId = reviewerId;
    }
}
