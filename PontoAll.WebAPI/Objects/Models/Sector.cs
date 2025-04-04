using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("sector")]
public class Sector
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")] 
    public string Name { get; set; }

    public Sector() { }
    public Sector(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
