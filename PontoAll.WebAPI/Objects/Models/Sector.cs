using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("sector")]
public class Sector
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")] 
    public string Name { get; set; }

    [Column("department")]
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null;

    public Sector() { }
    public Sector(int id, string name, int departmentId)
    {
        Id = id;
        Name = name;
        DepartmentId = departmentId;
    }
}
