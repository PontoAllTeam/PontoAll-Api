using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Data.Builders;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WorkSchedule> WorkSchedules { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<TimeRecord> TimeRecords { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<BiometricData> BiometricDatas { get; set; }
    public DbSet<PermissionRequest> PermissionRequests { get; set; }
    public DbSet<DailyRecord> DailyRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        WorkScheduleBuilder.Build(modelBuilder);
        CompanyBuilder.Build(modelBuilder);
        UserBuilder.Build(modelBuilder);
        DepartmentBuilder.Build(modelBuilder);
        SectorBuilder.Build(modelBuilder);
        TimeRecordBuilder.Build(modelBuilder);
    }
}
