using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class CompanyBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasKey(c => c.Id);
        modelBuilder.Entity<Company>().Property(c => c.CorporateName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(c => c.FantasyName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(c => c.Cnpj).IsRequired().HasMaxLength(14);
        modelBuilder.Entity<Company>().Property(c => c.Email).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Company>().Property(c => c.BusinessPhone).IsRequired().HasMaxLength(11);
        modelBuilder.Entity<Company>().Property(c => c.State).IsRequired().HasMaxLength(2);
        modelBuilder.Entity<Company>().Property(c => c.City).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(c => c.Cep).IsRequired().HasMaxLength(8);
        modelBuilder.Entity<Company>().Property(c => c.Street).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(c => c.Neighborhood).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Company>().Property(c => c.Number).IsRequired().HasMaxLength(5);
        modelBuilder.Entity<Company>().Property(c => c.CompanyStatus).IsRequired();

        modelBuilder.Entity<Company>().HasData(new List<Company>
        {
            new(1, "Coca Cola Industrias Ltda", "Coca Cola", "45997418000153", "cobranca@kof.com.mx", "11976212763", "SP", "Jundiaí", "13212240", "Rodovia Dom Gabriel Paulino Bueno Couto", "Jardim Tannus", 0, CompanyStatus.ACTIVE),
        });
    }
}
