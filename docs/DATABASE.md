# Database Design

This document specifies the database schema for the license table and the setup using Entity Framework Core.

## Database Technology

*   **Provider:** PostgreSQL
*   **ORM:** Entity Framework Core 8

## License Table Schema

The primary table will be `Licenses`.

| Column Name             | Data Type          | Constraints              | Description                                      |
| ----------------------- | ------------------ | ------------------------ | ------------------------------------------------ |
| `Id`                    | `uuid`             | `PRIMARY KEY`            | The unique identifier for the license record.    |
| `LicenseKey`            | `varchar(255)`     | `NOT NULL`, `UNIQUE`     | The generated software license key.              |
| `CustomerEmail`         | `varchar(255)`     | `NOT NULL`               | The email of the customer who made the purchase. |
| `PurchaseDate`          | `timestamp`        | `NOT NULL`               | The date and time of the purchase.               |
| `ExpirationDate`        | `timestamp`        | `NULL`                   | For future use with subscription plans.          |
| `IsActive`              | `boolean`          | `NOT NULL`, `DEFAULT true` | Whether the license is currently active.         |
| `StripeCheckoutSessionId` | `varchar(255)`     | `NOT NULL`               | The ID from the Stripe Checkout session.         |

## Entity Framework Core Setup

### 1. Domain Model

The C# class representing the `Licenses` table.

```csharp
// Location: src/EZPos.Web.Domain/License.cs

public class License
{
    public Guid Id { get; set; }
    public string LicenseKey { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public string StripeCheckoutSessionId { get; set; }
}
```

### 2. DbContext

The `DbContext` class is the bridge between our domain models and the database.

```csharp
// Location: src/EZPos.Web.Infra/Data/ApplicationDbContext.cs

using EZPos.Web.Domain;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<License> Licenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.LicenseKey).IsUnique();
            entity.Property(e => e.LicenseKey).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(255);
            entity.Property(e => e.StripeCheckoutSessionId).IsRequired().HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });
    }
}
```

### 3. Configuration

In the `EZPos.Web.Api` project, we will configure the `DbContext` in `Program.cs`.

```csharp
// Location: src/EZPos.Web.Api/Program.cs

// ... other using statements
using Microsoft.EntityFrameworkCore;
using EZPos.Web.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// ... rest of the file
```

This setup allows us to use EF Core's migration tools to automatically create and update the database schema based on our C# models.
