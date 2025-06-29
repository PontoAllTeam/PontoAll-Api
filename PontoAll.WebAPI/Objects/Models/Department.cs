using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("department")]
public class Department
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("companyid")]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public Department() { }
    public Department(int id, string name, int companyId)
    {
        Id = id;
        Name = name;
        CompanyId = companyId;
    }

}