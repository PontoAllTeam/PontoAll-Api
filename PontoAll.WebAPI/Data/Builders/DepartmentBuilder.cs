using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;
public class DepartmentBuilder
{
    public static void Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasKey(s => s.Id);
        modelBuilder.Entity<Department>().Property(s => s.Name).IsRequired().HasMaxLength(100);

    }
}
