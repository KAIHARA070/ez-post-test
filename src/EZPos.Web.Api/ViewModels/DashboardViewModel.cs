using EZPos.Web.Domain;

namespace EZPos.Web.Api.ViewModels;

/// <summary>
/// ViewModel for the Customer Dashboard (future feature).
/// </summary>
public class DashboardViewModel
{
    public List<LicenseViewModel> Licenses { get; set; } = new();
    public int TotalLicenses { get; set; }
    public int ActiveLicenses { get; set; }
}

/// <summary>
/// Display model for a single license in the dashboard.
/// </summary>
public class LicenseViewModel
{
    public Guid Id { get; set; }
    public string LicenseKey { get; set; } = string.Empty;
    public string LicenseType { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public string Status => IsActive ? "Active" : "Inactive";
}
