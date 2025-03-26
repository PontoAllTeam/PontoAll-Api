using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class CompanyBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasKey(s => s.Id);
        modelBuilder.Entity<Company>().Property(s => s.CorporateName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(s => s.FantasyName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(s => s.Cnpj).IsRequired().HasMaxLength(14);
        modelBuilder.Entity<Company>().Property(s => s.Email).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Company>().Property(s => s.BusinessPhone).IsRequired().HasMaxLength(11);
        modelBuilder.Entity<Company>().Property(s => s.State).IsRequired().HasMaxLength(2);
        modelBuilder.Entity<Company>().Property(s => s.City).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(s => s.Cep).IsRequired().HasMaxLength(8);
        modelBuilder.Entity<Company>().Property(s => s.Street).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(s => s.Neighborhood).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(s => s.Number).IsRequired().HasMaxLength(5);
        modelBuilder.Entity<Company>().Property(s => s.Status).IsRequired();
    }
}
