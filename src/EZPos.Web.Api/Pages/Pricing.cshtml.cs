using Microsoft.AspNetCore.Mvc.RazorPages;
using EZPos.Web.Api.Services;
using EZPos.Web.Api.ViewModels;

namespace EZPos.Web.Api.Pages;

public class PricingModel : PageModel
{
    private readonly ILicenseApiService _licenseApiService;
    private readonly ILogger<PricingModel> _logger;

    public PricingViewModel Pricing { get; set; } = new();

    public PricingModel(ILicenseApiService licenseApiService, ILogger<PricingModel> logger)
    {
        _licenseApiService = licenseApiService;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        try
        {
            Pricing = await _licenseApiService.GetPricingAsync();
            _logger.LogInformation("Pricing page loaded successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading pricing");
            Pricing = new PricingViewModel();
        }
    }
}
