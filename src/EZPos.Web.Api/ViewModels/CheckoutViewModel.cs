using System.ComponentModel.DataAnnotations;

namespace EZPos.Web.Api.ViewModels;

/// <summary>
/// ViewModel for the Checkout page.
/// </summary>
public class CheckoutViewModel
{
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "License type is required.")]
    public string LicenseType { get; set; } = "Professional";

    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = "USD";
    public string PlanName { get; set; } = string.Empty;
    public bool AgreeToTerms { get; set; }
}
