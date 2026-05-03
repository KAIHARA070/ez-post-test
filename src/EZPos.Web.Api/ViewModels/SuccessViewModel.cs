namespace EZPos.Web.Api.ViewModels;

/// <summary>
/// ViewModel for the Success page (after payment confirmation).
/// </summary>
public class SuccessViewModel
{
    public string Email { get; set; } = string.Empty;
    public string LicenseType { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string Message { get; set; } = "Your license key has been sent to your email.";
    public bool IsSuccessful { get; set; } = true;
}
