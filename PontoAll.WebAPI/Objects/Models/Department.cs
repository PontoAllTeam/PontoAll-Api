using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("department")]
public class Department
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    public Department() { }
    public Department(int id, string name)
    {
        Id = id;
        Name = name;
    }

}