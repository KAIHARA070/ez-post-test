using EZPos.Web.Api.ViewModels;

namespace EZPos.Web.Api.Services;

/// <summary>
/// Implementation of ILicenseApiService using HttpClient.
/// </summary>
public class LicenseApiService : ILicenseApiService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<LicenseApiService> _logger;

    public LicenseApiService(IConfiguration configuration, ILogger<LicenseApiService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<PricingViewModel> GetPricingAsync()
    {
        try
        {
            // For now, return static pricing. In Phase 2, this will call the backend API.
            var pricing = new PricingViewModel
            {
                PageTitle = "EZPos Licensing",
                PageDescription = "Professional POS system licensing",
                Tiers = new List<PricingTierViewModel>
                {
                    new PricingTierViewModel
                    {
                        Name = "Professional License",
                        Description = "Complete POS system with all features",
                        Price = 199.99m,
                        Currency = "USD",
                        BillingPeriod = "One-time purchase",
                        IsRecommended = true,
                        Features = new List<string>
                        {
                            "Unlimited transactions",
                            "Multi-user support",
                            "Inventory management",
                            "Sales reporting",
                            "Email support",
                            "Regular updates"
                        }
                    }
                }
            };

            _logger.LogInformation("Pricing retrieved successfully");
            return await Task.FromResult(pricing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pricing");
            throw;
        }
    }

    public async Task<bool> ValidateLicenseAsync(string licenseKey)
    {
        try
        {
            _logger.LogInformation("Validating license key: {LicenseKey}", licenseKey);
            // Placeholder: Will be implemented in Phase 1 with actual API call
            await Task.Delay(10); // Simulate async operation
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating license");
            return false;
        }
    }

    public async Task<LicenseViewModel?> GetLicenseDetailsAsync(string licenseKey)
    {
        try
        {
            _logger.LogInformation("Retrieving license details: {LicenseKey}", licenseKey);
            // Placeholder: Will be implemented in Phase 1 with actual API call
            await Task.Delay(10);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving license details");
            return null;
        }
    }
}
