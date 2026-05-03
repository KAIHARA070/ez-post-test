using Microsoft.AspNetCore.Mvc.RazorPages;
using EZPos.Web.Api.Services;
using EZPos.Web.Api.ViewModels;

namespace EZPos.Web.Api.Pages;

public class SuccessModel : PageModel
{
    private readonly IStripeService _stripeService;
    private readonly ILogger<SuccessModel> _logger;

    public SuccessViewModel Success { get; set; } = new();

    public SuccessModel(IStripeService stripeService, ILogger<SuccessModel> logger)
    {
        _stripeService = stripeService;
        _logger = logger;
    }

    public async Task OnGetAsync(string? session_id)
    {
        try
        {
            if (string.IsNullOrEmpty(session_id))
            {
                _logger.LogWarning("Success page accessed without session_id");
                Success.IsSuccessful = false;
                Success.Message = "Invalid session. Please contact support.";
                return;
            }

            var isValid = await _stripeService.VerifyCheckoutSessionAsync(session_id);

            if (isValid)
            {
                // In Phase 2, we'll retrieve actual customer email from session
                Success.Email = "customer@example.com";
                Success.IsSuccessful = true;
                _logger.LogInformation("Payment verified for session: {SessionId}", session_id);
            }
            else
            {
                Success.IsSuccessful = false;
                Success.Message = "Payment verification failed. Please contact support.";
                _logger.LogWarning("Failed to verify payment for session: {SessionId}", session_id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing success page");
            Success.IsSuccessful = false;
            Success.Message = "An error occurred. Please contact support.";
        }
    }
}
