using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class SectorBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sector>().HasKey(s => s.Id);
        modelBuilder.Entity<Sector>().Property(s => s.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Sector>().HasData(new List<Sector>
        {
            new(1, "Recrutamento e Seleção", 2),
        });
    }
}
