using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("permissionrequest")]
public class PermissionRequest
{
    [Column("id")]
    public int Id { get; set; }

    [Column("requestcomments")]
    public string RequestComments { get; set; }

    [Column("reviewcomments")]
    public string? ReviewComments { get; set; }

    [Column("permissiontype")]
    public PermissionType PermissionType { get; set; }

    [Column("permissionstatus")]
    public PermissionStatus PermissionStatus { get; set; }

    [Column("requestdate")]
    public DateTime RequestDate { get; set; }

    [Column("reviewdate")]
    public DateTime? ReviewDate { get; set; }

    [Column("requesterid")]
    public int RequesterId { get; set; }
    public User Requester { get; set; } = null!;

    [Column("reviewerid")]
    public int? ReviewerId { get; set; }
    public User Reviewer { get; set; } = null!;

    public PermissionRequest() { }

    public PermissionRequest(int id, int requesterId, int? reviewerId, string requestComments, string? reviewComments, PermissionType permissionType, PermissionStatus permissionStatus, DateTime requestDate, DateTime? reviewDate)
    {
        Id = id;
        RequesterId = requesterId;
        ReviewerId = reviewerId;
        RequestComments = requestComments;
        ReviewComments = reviewComments;
        PermissionType = permissionType;
        PermissionStatus = permissionStatus;
        RequestDate = requestDate;
        ReviewDate = reviewDate;
    }
}
