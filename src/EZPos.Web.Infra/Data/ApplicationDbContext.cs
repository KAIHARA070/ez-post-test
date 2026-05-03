using EZPos.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace EZPos.Web.Infra.Data;

/// <summary>
/// Entity Framework Core DbContext for EZPos License Management System.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ApplicationDbContext.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DbSet for License entities.
    /// </summary>
    public DbSet<License> Licenses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.LicenseKey)
                .IsUnique()
                .HasDatabaseName("IX_License_LicenseKey");

            entity.HasIndex(e => e.CustomerEmail)
                .HasDatabaseName("IX_License_CustomerEmail");

            entity.HasIndex(e => e.StripeCheckoutSessionId)
                .HasDatabaseName("IX_License_StripeSessionId");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LicenseKey)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.CustomerEmail)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.StripeCheckoutSessionId)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.StripeCustomerId)
                .HasMaxLength(255);

            entity.Property(e => e.LicenseType)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("Professional");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("NOW()");
        });
    }
}
