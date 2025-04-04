using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class SectorBuilder
{
    public static void Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sector>().HasKey(s => s.Id);
        modelBuilder.Entity<Sector>().Property(s => s.Name).IsRequired().HasMaxLength(100);
    }
}
