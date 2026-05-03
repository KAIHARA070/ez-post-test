using EZPos.Web.Api.ViewModels;

namespace EZPos.Web.Api.Services;

/// <summary>
/// Interface for interacting with the License API.
/// </summary>
public interface ILicenseApiService
{
    /// <summary>
    /// Gets available pricing tiers.
    /// </summary>
    Task<PricingViewModel> GetPricingAsync();

    /// <summary>
    /// Validates a license key.
    /// </summary>
    Task<bool> ValidateLicenseAsync(string licenseKey);

    /// <summary>
    /// Gets details about a specific license.
    /// </summary>
    Task<LicenseViewModel?> GetLicenseDetailsAsync(string licenseKey);
}
