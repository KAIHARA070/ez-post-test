using Microsoft.EntityFrameworkCore;
using EZPos.Web.Infra.Data;
using EZPos.Web.Api.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// ===== Configuration =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// ===== Add Services =====

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, x =>
    {
        x.MigrationsHistoryTable("__EFMigrationsHistory");
    }));

// Application Services
builder.Services.AddScoped<ILicenseApiService, LicenseApiService>();
builder.Services.AddScoped<IStripeService, StripeService>();

// Razor Pages
builder.Services.AddRazorPages();

// Logging
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

// ===== Build App =====
var app = builder.Build();

// ===== Configure HTTP Pipeline =====

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ===== Map Endpoints =====
app.MapRazorPages();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithName("Health");

app.Run();
