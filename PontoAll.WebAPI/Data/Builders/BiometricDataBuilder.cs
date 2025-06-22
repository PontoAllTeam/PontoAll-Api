using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class BiometricDataBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BiometricData>().HasKey(b => b.Id);
        modelBuilder.Entity<BiometricData>().Property(b => b.FacialEmbedding).HasColumnType("bytea").IsRequired();
        modelBuilder.Entity<BiometricData>().Property(b => b.CreatedAt).IsRequired();
        modelBuilder.Entity<BiometricData>().Property(b => b.UserId).IsRequired();

        modelBuilder.Entity<BiometricData>().HasData(new List<BiometricData>
        {
            new(1, new float[] { 0.123f, 0.456f, 0.789f }, new DateTime(2025, 6, 22), 1),
        });
    }
}
