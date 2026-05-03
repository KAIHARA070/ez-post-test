namespace EZPos.Web.Domain;

/// <summary>
/// Represents a software license for EZPos POS System.
/// </summary>
public class License
{
    /// <summary>
    /// Unique identifier for the license record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique, generated license key that customers use in the installer.
    /// </summary>
    public string LicenseKey { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the customer who purchased this license.
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the license was purchased.
    /// </summary>
    public DateTime PurchaseDate { get; set; }

    /// <summary>
    /// The date when the license expires (for future subscription plans).
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Whether the license is currently active and valid.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The Stripe Checkout Session ID associated with this purchase.
    /// </summary>
    public string StripeCheckoutSessionId { get; set; } = string.Empty;

    /// <summary>
    /// The Stripe Customer ID for future reference.
    /// </summary>
    public string? StripeCustomerId { get; set; }

    /// <summary>
    /// License type or edition (e.g., "Professional", "Enterprise").
    /// </summary>
    public string LicenseType { get; set; } = "Professional";
}
