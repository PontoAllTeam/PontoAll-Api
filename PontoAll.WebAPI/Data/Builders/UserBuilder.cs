using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class UserBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(s => s.Id);
        modelBuilder.Entity<User>().Property(s => s.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Cpf).IsRequired().HasMaxLength(11);
        modelBuilder.Entity<User>().Property(s => s.Phone).IsRequired().HasMaxLength(11);
        modelBuilder.Entity<User>().Property(s => s.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.RecoveryEmail).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Registration).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Password).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.UserType).IsRequired();
        modelBuilder.Entity<User>().Property(s => s.UserStatus).IsRequired();
        modelBuilder.Entity<User>().Property(s => s.CompanyId).IsRequired();
        modelBuilder.Entity<User>().Property(s => s.SectorId).IsRequired();

        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new(1, "Carlos", "12312312389", "12798798798", "carlos.gabriel@gmail.com", "carlin@outlook.com", "241251251", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserType.DIRECTOR, UserStatus.ACTIVE, 1, 1),
            new(2, "Lucas", "23498712390", "23498712390", "lucas.monteiro@gmail.com", "monteiro@outlook.com", "241251252", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserType.MANAGER, UserStatus.ACTIVE, 1, 1),
        });
    }
}
