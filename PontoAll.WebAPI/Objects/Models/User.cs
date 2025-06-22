using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("cpf")]
    public string Cpf { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("recoveryemail")]
    public string RecoveryEmail { get; set; }

    [Column("registration")]
    public string Registration { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("usertype")]
    public UserType UserType { get; set; }

    [Column("userstatus")]
    public UserStatus UserStatus { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public User() { }
    public User(int id, string name, string cpf, string phone, string email, string recoveryEmail, string registration, string password, UserType userType, UserStatus userStatus, int companyId)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Phone = phone;
        Email = email;
        RecoveryEmail = recoveryEmail;
        Registration = registration;
        Password = password;
        UserType = userType;
        UserStatus = userStatus;
        CompanyId = companyId;
    }
}
