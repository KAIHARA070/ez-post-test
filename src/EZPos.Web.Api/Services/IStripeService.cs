namespace EZPos.Web.Api.Services;

/// <summary>
/// Interface for Stripe payment operations.
/// </summary>
public interface IStripeService
{
    /// <summary>
    /// Creates a Stripe Checkout session for one-time payment.
    /// </summary>
    Task<string> CreateCheckoutSessionAsync(string email, string licenseType, decimal amount);

    /// <summary>
    /// Retrieves a checkout session by ID.
    /// </summary>
    Task<bool> VerifyCheckoutSessionAsync(string sessionId);

    /// <summary>
    /// Verifies and processes a Stripe webhook event.
    /// </summary>
    Task<bool> VerifyWebhookSignatureAsync(string json, string signatureHeader);
}
