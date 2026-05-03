namespace EZPos.Web.Api.ViewModels;

/// <summary>
/// Represents pricing information for a single plan.
/// </summary>
public class PricingTierViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public string BillingPeriod { get; set; } = "One-time";
    public bool IsRecommended { get; set; }
    public List<string> Features { get; set; } = new();
}

/// <summary>
/// ViewModel for the Pricing page.
/// </summary>
public class PricingViewModel
{
    public List<PricingTierViewModel> Tiers { get; set; } = new();
    public string PageTitle { get; set; } = "Pricing";
    public string PageDescription { get; set; } = "Simple, transparent pricing";
}
