using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Data.Builders;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Scale> Scales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ScaleBuilder.Build(modelBuilder);
    }
}
