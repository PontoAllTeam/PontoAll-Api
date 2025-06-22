using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Services.Utils;

namespace PontoAll.WebAPI.Data.Builders;

public class BiometricDataBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        var floatArrayToBytesConverter = new ValueConverter<float[], byte[]>(
            v => ConverterUtils.FloatArrayToByteArray(v),
            v => ConverterUtils.ByteArrayToFloatArray(v));

        modelBuilder.Entity<BiometricData>().HasKey(b => b.Id);
        modelBuilder.Entity<BiometricData>()
            .Property(b => b.FacialEmbedding)
            .HasConversion(floatArrayToBytesConverter)
            .HasColumnType("bytea").IsRequired();
        modelBuilder.Entity<BiometricData>().Property(b => b.CreatedAt).IsRequired();
        modelBuilder.Entity<BiometricData>().Property(b => b.UserId).IsRequired();

        modelBuilder.Entity<BiometricData>().HasData(new List<BiometricData>
        {
            new(1, new float[] { 0.123f, 0.456f, 0.789f }, new DateTime(2025, 6, 22), 1),
        });
    }
}
