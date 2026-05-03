# UI Project Structure

This document outlines the folder structure for the UI components of the EZPos Web + License Key System, integrated into the `EZPos.Web.Api` project using ASP.NET Core Razor Pages.

## Directory Layout

```
src/EZPos.Web.Api/
|
|-- Pages/
|   |-- Shared/
|   |   |-- _Layout.cshtml           # Master layout template
|   |   |-- _Navigation.cshtml       # Navigation component
|   |   |-- _Footer.cshtml           # Footer component
|   |   |-- _ValidationScriptsPartial.cshtml  # Validation scripts
|   |
|   |-- Index.cshtml                 # Landing page
|   |-- Index.cshtml.cs              # Landing page code-behind
|   |
|   |-- Pricing.cshtml               # Pricing/Plans page
|   |-- Pricing.cshtml.cs            # Pricing page code-behind
|   |
|   |-- Checkout.cshtml              # Checkout page (pre-payment)
|   |-- Checkout.cshtml.cs           # Checkout page code-behind
|   |
|   |-- Success.cshtml               # Payment success page
|   |-- Success.cshtml.cs            # Success page code-behind
|   |
|   |-- Dashboard.cshtml             # Customer dashboard (future feature)
|   |-- Dashboard.cshtml.cs          # Dashboard code-behind
|
|-- Components/
|   |-- PricingCard.razor            # Reusable pricing card component
|   |-- FeatureHighlight.razor       # Feature highlight component
|   |-- Testimonial.razor            # Testimonial component (optional)
|
|-- wwwroot/
|   |-- css/
|   |   |-- bootstrap.min.css        # Bootstrap 5 CSS framework
|   |   |-- site.css                 # Custom site styling
|   |   |-- variables.css            # CSS custom properties (theming)
|   |
|   |-- js/
|   |   |-- bootstrap.bundle.min.js  # Bootstrap 5 JavaScript
|   |   |-- site.js                  # Custom site JavaScript
|   |   |-- stripe-integration.js    # Stripe integration logic
|   |
|   |-- images/                      # Product images, logos, icons
|   |-- fonts/                       # Custom fonts (if needed)
|
|-- ViewModels/
|   |-- PricingViewModel.cs          # Model for pricing page
|   |-- CheckoutViewModel.cs         # Model for checkout page
|   |-- LicenseViewModel.cs          # Model for displaying license info
|   |-- DashboardViewModel.cs        # Model for dashboard
|
|-- Services/
|   |-- ILicenseApiClient.cs         # Interface for License API calls
|   |-- LicenseApiClient.cs          # Implementation using HttpClient
|   |-- IStripeService.cs            # Interface for Stripe operations
|   |-- StripeService.cs             # Stripe service implementation
|
|-- appsettings.json                 # Configuration (API endpoints, Stripe keys)
|-- Program.cs                       # Application startup configuration
```

## Folder Descriptions

### `Pages/`
Contains all Razor Pages (`.cshtml` files) and their corresponding code-behind files (`.cshtml.cs`). Each page represents a distinct view in the application.

**Shared Folder:**
- **`_Layout.cshtml`** - The master layout template that all pages inherit from. Contains HTML structure, navigation, and footer.
- **`_Navigation.cshtml`** - Partial view for the navigation bar. Separated for easier maintenance.
- **`_Footer.cshtml`** - Partial view for the footer.
- **`_ValidationScriptsPartial.cshtml`** - Partial view containing client-side validation scripts.

### `Components/`
Contains Razor Components (`.razor` files) that are reusable UI elements. These can be used across multiple pages.

### `wwwroot/`
Static files served directly to the browser. Organized by type:
- **`css/`** - Stylesheets
- **`js/`** - JavaScript files
- **`images/`** - Images and icons
- **`fonts/`** - Custom font files

### `ViewModels/`
C# classes that represent the data structure passed from Razor Page code-behind to the view. Each model is specific to its corresponding page.

### `Services/`
Contains service classes that handle:
- **API Communication:** Calls to the License API endpoints.
- **Third-Party Integrations:** Stripe API interaction.

These are designed to be injected via dependency injection in the page code-behind.

## Design Principles

1. **Separation of Concerns:** Each page has a code-behind file that handles logic and API calls.
2. **Reusability:** Common UI elements are extracted into components in the `Components/` folder.
3. **Configuration-Driven:** API endpoints and Stripe keys are stored in `appsettings.json` and injected at runtime.
4. **Clean Architecture:** Services abstract external dependencies (API, Stripe), making the UI layer testable.
