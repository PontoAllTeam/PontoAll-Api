using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class UserBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(s => s.Id);
        modelBuilder.Entity<User>().Property(s => s.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Cpf).IsRequired().HasMaxLength(11);
        modelBuilder.Entity<User>().Property(s => s.Phone).IsRequired().HasMaxLength(15);
        modelBuilder.Entity<User>().Property(s => s.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.RecoveryEmail).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Registration).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Password).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Type).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(s => s.Status).IsRequired().HasMaxLength(100);
    }
}
