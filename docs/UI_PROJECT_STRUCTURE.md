# UI Project Structure

This document defines the recommended folder structure for the EZPos customer-facing UI built with Blazor Web App (Server interactivity) and Microsoft Fluent UI Blazor components.

## Recommended Layout

```
src/EZPos.Web.Ui/
|
|-- Components/
|   |-- App.razor
|   |-- Routes.razor
|   |-- _Imports.razor
|   |
|   |-- Layout/
|   |   |-- MainLayout.razor
|   |   |-- MainLayout.razor.css
|   |   |-- NavMenu.razor
|   |
|   |-- Pages/
|   |   |-- Landing.razor
|   |   |-- Pricing.razor
|   |   |-- NotFound.razor
|   |   |-- Error.razor
|   |
|   |-- Shared/
|   |   |-- ThemeToggle.razor
|   |   |-- HeroSection.razor
|   |   |-- FeatureCard.razor
|   |   |-- PricingCard.razor
|
|-- Services/
|   |-- Api/
|   |   |-- ILicenseApiClient.cs
|   |   |-- LicenseApiClient.cs
|   |-- State/
|   |   |-- ThemeState.cs
|
|-- Models/
|   |-- Ui/
|   |   |-- PlanViewModel.cs
|   |   |-- FeatureViewModel.cs
|
|-- wwwroot/
|   |-- app.css
|   |-- styles/
|   |   |-- tokens.css
|   |   |-- fluent-surfaces.css
|   |   |-- landing.css
|   |-- js/
|   |   |-- theme.js
|   |-- images/
|   |   |-- brand/
|   |   |-- illustrations/
|
|-- appsettings.json
|-- appsettings.Development.json
|-- Program.cs
|-- EZPos.Web.Ui.csproj
```

## Layer Responsibilities

- Components/Layout: Global frame of the app (navigation, content container, footer).
- Components/Pages: Route-based pages only; keep heavy UI pieces in Shared components.
- Components/Shared: Reusable Fluent UI building blocks used across multiple pages.
- Services/Api: HTTP clients that consume EZPos license endpoints.
- Services/State: UI state like theme mode and user preferences.
- Models/Ui: View models for rendering cards, sections, and summaries.
- wwwroot/styles: Design token and visual surface system (dark-first + light mode).
- wwwroot/js: Small behavior scripts (theme persistence, optional smooth scrolling).

## Future-Proofing Rules

1. Add new pages in Components/Pages and keep them route-focused.
2. Avoid page-specific inline CSS; place style tokens and section styles in wwwroot/styles.
3. API calls go through Services/Api only; pages must not call HttpClient directly.
4. Shared cards, headers, and call-to-action blocks must live in Components/Shared.
5. Keep naming consistent by feature: PricingCard, DashboardShell, CustomerPortalHeader.
