# UI Implementation Guide

This document provides setup instructions and integration details for the EZPos Landing Page and Checkout UI built with ASP.NET Core Razor Pages.

## Overview

The UI layer is integrated directly into the `EZPos.Web.Api` project using **ASP.NET Core Razor Pages** with **Bootstrap 5** for responsive, modern design.

---

## Prerequisites

- .NET 8 SDK installed
- Visual Studio 2022 or VS Code with C# extension
- Understanding of ASP.NET Core Razor Pages
- PostgreSQL database running (for License API backend)

---

## Project Setup

### 1. Add Razor Pages Support

In the `EZPos.Web.Api/Program.cs`, ensure Razor Pages are configured:

```csharp
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
```

### 2. Install Required NuGet Packages

The following packages are already included with the Web API template:

- `Microsoft.AspNetCore.Mvc.Razor` (Razor Pages support)
- `Bootstrap` (CSS framework)

No additional packages needed for basic Razor Pages + Bootstrap integration.

---

## Configuration

### appsettings.json

Add the following configuration sections:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ezpos_licenses;Username=postgres;Password=yourpassword"
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_...",
    "WebhookSecret": "whsec_..."
  },
  "LicenseApi": {
    "BaseUrl": "https://localhost:7001",
    "ApiKey": "your-api-key-here"
  },
  "App": {
    "Name": "EZPos License Management",
    "Version": "1.0.0"
  }
}
```

---

## Folder Structure Setup

Create the following directories in `src/EZPos.Web.Api/`:

```
Pages/
├── Shared/
├── (individual .cshtml files)

Components/
├── (Razor component files)

Services/
├── (Service classes)

ViewModels/
├── (ViewModel classes)

wwwroot/
├── css/
├── js/
└── images/
```

---

## Page Breakdown

### Landing Page (`Pages/Index.cshtml`)

**Purpose:** Introduce the product, showcase features, and drive users to pricing/checkout.

**Content Sections:**
- Hero section with CTA
- Features overview
- Why choose EZPos section
- Footer

**Key Features:**
- Responsive design (mobile-first)
- Smooth scroll animations (via CSS)
- Clear navigation to pricing page

### Pricing Page (`Pages/Pricing.cshtml`)

**Purpose:** Display available plans and facilitate plan selection.

**Features:**
- Pricing card components
- Feature comparison (for future multi-tier plans)
- "Buy Now" buttons linked to checkout

### Checkout Page (`Pages/Checkout.cshtml`)

**Purpose:** Collect customer information and initiate Stripe payment.

**Features:**
- Email input field with validation
- Order summary display
- "Proceed to Payment" button
- Terms acceptance checkbox

**Integration:**
- Calls `StripeService.CreateCheckoutSessionAsync()`
- Redirects to Stripe Checkout URL

### Success Page (`Pages/Success.cshtml`)

**Purpose:** Confirm payment and guide customer to next steps.

**Features:**
- Success confirmation message
- Email verification display
- Download and support links

---

## Services Layer

### LicenseApiClient

**Purpose:** HTTP communication with License API.

**Implementation:**
```csharp
public class LicenseApiClient : ILicenseApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public LicenseApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<PricingResponse> GetPricingAsync()
    {
        var url = _configuration["LicenseApi:BaseUrl"] + "/api/pricing";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<PricingResponse>();
    }
}
```

### StripeService

**Purpose:** Stripe payment session creation and webhook handling.

**Implementation:**
```csharp
public class StripeService : IStripeService
{
    private readonly IConfiguration _configuration;

    public StripeService(IConfiguration configuration)
    {
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<string> CreateCheckoutSessionAsync(string email, string licenseType)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 19999, // $199.99 in cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "EZPos License",
                            Description = $"License Type: {licenseType}"
                        }
                    },
                    Quantity = 1
                }
            },
            CustomerEmail = email,
            SuccessUrl = "https://yourdomain.com/success",
            CancelUrl = "https://yourdomain.com/pricing",
            Mode = "payment"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session.Url;
    }
}
```

---

## Running the Application

### 1. Restore Dependencies
```bash
cd src/EZPos.Web.Api
dotnet restore
```

### 2. Apply Database Migrations
```bash
dotnet ef database update
```

### 3. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:7001` (or configured port).

### 4. Access Pages
- **Landing Page:** `https://localhost:7001/`
- **Pricing:** `https://localhost:7001/pricing`
- **Checkout:** `https://localhost:7001/checkout`
- **Success:** `https://localhost:7001/success`

---

## Integration with License API

### API Endpoints Used by UI

| Endpoint | Method | Purpose |
|----------|--------|---------|
| `/api/licenses/validate` | POST | Validate a license key |
| `/api/pricing` | GET | Retrieve pricing information |
| `/api/webhooks/stripe` | POST | Handle Stripe webhook events |

### Frontend to Backend Communication

1. **Razor Pages (Frontend)** calls service methods
2. **Services** make HTTP calls to backend API
3. **API** returns JSON responses
4. **Razor Pages** render responses in views

---

## Deployment

### Prerequisites
- Azure App Service or similar hosting
- PostgreSQL database in production
- Stripe account with production keys

### Environment Configuration
Create `appsettings.Production.json` with production values:
- Production database connection string
- Stripe production keys
- Production API base URL

### Publishing
```bash
dotnet publish -c Release -o ./publish
```

---

## Next Steps

1. Implement Razor Page files and code-behind classes
2. Create Bootstrap layout and styling
3. Integrate with Stripe payment processing
4. Set up email notification service for license key delivery
5. Implement error handling and logging
6. Add security features (CORS, CSRF protection, rate limiting)
7. Perform end-to-end testing with Stripe sandbox environment
