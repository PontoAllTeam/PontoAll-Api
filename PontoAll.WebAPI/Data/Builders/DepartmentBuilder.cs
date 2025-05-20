using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;
public class DepartmentBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasKey(s => s.Id);
        modelBuilder.Entity<Department>().Property(s => s.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Department>().HasData(new List<Department>
        {
            new(1, "Financeiro", 1),
            new(2, "Recursos Humanos", 1),
        });
    }
}
