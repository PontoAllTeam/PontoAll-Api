using Microsoft.EntityFrameworkCore;
using PontoAll.WebAPI.Objects.Enums;
using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Data.Builders;

public class PermissionRequestBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionRequest>().HasKey(pr => pr.Id);
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.RequestComments).IsRequired();
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.ReviewComments);
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.PermissionType).IsRequired();
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.PermissionStatus).IsRequired();
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.RequestDate).IsRequired();
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.ReviewDate);
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.RequesterId).IsRequired();
        modelBuilder.Entity<PermissionRequest>().Property(pr => pr.ReviewerId);

        modelBuilder.Entity<PermissionRequest>().HasData(new List<PermissionRequest>
        {
            new(1, 1, 1, "Preciso cadastrar um novo gerente", "Aprovado", PermissionType.CREATE_MANAGER, PermissionStatus.APPROVED, new DateTime(2025, 6, 18), new DateTime(2025, 6, 30)),
        });
    }
}
