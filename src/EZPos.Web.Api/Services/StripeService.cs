using Stripe;
using Stripe.Checkout;

namespace EZPos.Web.Api.Services;

/// <summary>
/// Implementation of IStripeService for handling Stripe payment operations.
/// </summary>
public class StripeService : IStripeService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<StripeService> _logger;

    public StripeService(IConfiguration configuration, ILogger<StripeService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<string> CreateCheckoutSessionAsync(string email, string licenseType, decimal amount)
    {
        try
        {
            _logger.LogInformation("Creating Stripe checkout session for email: {Email}, type: {LicenseType}", email, licenseType);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                CustomerEmail = email,
                SuccessUrl = _configuration["App:BaseUrl"] + "/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = _configuration["App:BaseUrl"] + "/pricing",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // Convert to cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "EZPos " + licenseType,
                                Description = $"EZPos POS System - {licenseType} License",
                                Images = new List<string>
                                {
                                    _configuration["App:BaseUrl"] + "/images/ezpos-logo.png"
                                }
                            }
                        },
                        Quantity = 1
                    }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            _logger.LogInformation("Checkout session created successfully: {SessionId}", session.Id);
            return session.Url ?? throw new InvalidOperationException("No session URL returned from Stripe");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Stripe checkout session");
            throw;
        }
    }

    public async Task<bool> VerifyCheckoutSessionAsync(string sessionId)
    {
        try
        {
            _logger.LogInformation("Verifying Stripe checkout session: {SessionId}", sessionId);

            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            return session.PaymentStatus == "paid";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying checkout session: {SessionId}", sessionId);
            return false;
        }
    }

    public async Task<bool> VerifyWebhookSignatureAsync(string json, string signatureHeader)
    {
        try
        {
            _logger.LogInformation("Verifying Stripe webhook signature");

            var webhookSecret = _configuration["Stripe:WebhookSecret"];
            var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, webhookSecret);

            _logger.LogInformation("Webhook signature verified for event type: {EventType}", stripeEvent.Type);
            return true;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Error verifying Stripe webhook signature");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error verifying webhook signature");
            return false;
        }
    }
}
