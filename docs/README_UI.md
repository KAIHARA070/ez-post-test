# UI README

This guide explains how to run and extend the EZPos Blazor UI foundation powered by Fluent UI components.

## Tech Choice

- UI Framework: Blazor Web App with Server interactivity.
- Design Components: Microsoft.FluentUI.AspNetCore.Components.
- API Integration: HttpClient to EZPos license API.

## Tech Stack Summary

Live in this phase:

- C# with .NET 10
- Blazor Web App (Server interactivity)
- Fluent UI Blazor components
- Custom CSS token/surface system
- Small JS helper for theme persistence

Backend foundation (next phase):

- ASP.NET Core Web API (layered projects)
- EF Core + PostgreSQL provider
- Stripe .NET SDK for payment flow
- Render deployment with Docker fallback

Why this choice:
- Fast first paint and simple hosting.
- Native-like UI controls aligned with Fluent/WinUI.
- Easy future expansion without re-architecting.

## Run the UI

1. Restore solution packages.
2. Run the UI project.

Commands:

```bash
dotnet restore
dotnet run --project src/EZPos.Web.Ui/EZPos.Web.Ui.csproj
```

## Configure API Connection

Set API URL in src/EZPos.Web.Ui/appsettings.json:

```json
{
  "Api": {
    "BaseUrl": "https://localhost:7001"
  }
}
```

The UI should call backend endpoints through Services/Api client classes.

## Foundation Pages Included

- Home/Landing route.
- Pricing route.
- Shared MainLayout with header, theme toggle, and footer.

## Dark and Light Mode

- Default mode is dark.
- User can toggle in the header.
- Preference persists in browser localStorage.

## Expansion Guide

When adding new pages:

1. Create a new route component under Components/Pages.
2. Reuse shared cards and section components from Components/Shared.
3. Add API calls through Services/Api only.
4. Keep design consistent with tokens in wwwroot/styles.

Suggested next pages:

- Dashboard.razor
- CustomerPortal.razor
- PurchaseSuccess.razor
- LicenseActivation.razor

## Notes

- Payment logic is intentionally not implemented in this phase.
- This stage provides a visual and structural skeleton ready for business features.
