using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EZPos.Web.Api.Services;
using EZPos.Web.Api.ViewModels;

namespace EZPos.Web.Api.Pages;

public class CheckoutModel : PageModel
{
    private readonly IStripeService _stripeService;
    private readonly ILogger<CheckoutModel> _logger;

    [BindProperty]
    public CheckoutViewModel Checkout { get; set; } = new();

    public CheckoutModel(IStripeService stripeService, ILogger<CheckoutModel> logger)
    {
        _stripeService = stripeService;
        _logger = logger;
    }

    public void OnGet()
    {
        Checkout = new CheckoutViewModel
        {
            PlanName = "Professional License",
            TotalPrice = 199.99m,
            LicenseType = "Professional"
        };
        _logger.LogInformation("Checkout page loaded");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Checkout form validation failed");
                return Page();
            }

            if (!Checkout.AgreeToTerms)
            {
                ModelState.AddModelError("Checkout.AgreeToTerms", "You must agree to the terms of service.");
                return Page();
            }

            _logger.LogInformation("Creating Stripe checkout session for email: {Email}", Checkout.Email);

            var checkoutUrl = await _stripeService.CreateCheckoutSessionAsync(
                Checkout.Email,
                Checkout.LicenseType,
                199.99m
            );

            return Redirect(checkoutUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during checkout process");
            ModelState.AddModelError(string.Empty, "An error occurred during checkout. Please try again.");
            return Page();
        }
    }
}
