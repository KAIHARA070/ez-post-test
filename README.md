# EZPos Web Platform

EZPos is now based on a C# Blazor web frontend with a layered .NET backend foundation.

## Current Frontend

The customer-facing website runs from:

- src/EZPos.Web.Ui

This project is a Blazor Web App using Fluent UI components.

## Run Locally

1. Restore and build:

```bash
dotnet restore
dotnet build
```

2. Run frontend:

```bash
dotnet run --project src/EZPos.Web.Ui/EZPos.Web.Ui.csproj
```

3. Open browser:

- https://localhost:5001 (or the URL printed in terminal)

## Key Routes

- / -> landing page
- /pricing -> pricing page
- /not-found -> branded 404 route
- /healthz -> deployment health endpoint

## Hosting

Use the step-by-step deployment guide:

- docs/HOSTING_BLAZOR_RENDER.md

## Notes

- This repo now ignores future bin/ and obj/ output via .gitignore.
- Existing committed build artifacts can be cleaned in a later maintenance commit.
