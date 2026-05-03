# Hosting Guide: Full C# Blazor Web on Render

This guide deploys the Blazor web frontend from this repo.

Important:

- Root Directory: src/EZPos.Web.Ui deploys frontend only.
- To run full stack (frontend + backend), create a second Render service for API.

## 1) Prepare Repository

1. Ensure main branch has latest code.
2. Ensure project builds locally:

```bash
dotnet build src/EZPos.Web.Ui/EZPos.Web.Ui.csproj
```

## 2) Create Render Web Service

1. Login to Render dashboard.
2. Click New -> Web Service.
3. Connect GitHub repository:

- Reef-hash/EZPos-Landing

4. Configure service:

- Name: ezpos-web-ui
- Branch: main
- Root Directory: src/EZPos.Web.Ui

### If "Runtime: .NET" is available in your Render account

- Runtime: .NET
- Build Command: dotnet restore; dotnet publish -c Release -o out
- Start Command: dotnet out/EZPos.Web.Ui.dll

### If "Runtime: .NET" is NOT available

Use Docker deployment instead:

- Runtime: Docker
- Dockerfile Path: Dockerfile
- Build Command: leave empty
- Start Command: leave empty

This repo now includes:

- src/EZPos.Web.Ui/Dockerfile
- src/EZPos.Web.Ui/.dockerignore

## 3) Add Environment Variables

Set these in Render service settings:

- ASPNETCORE_ENVIRONMENT = Production
- Api__BaseUrl = https://your-api-domain.example.com

If API is not live yet, you can temporarily set:

- Api__BaseUrl = https://localhost:7001

Note:

- In Docker mode, container startup already binds to Render's PORT automatically.

## 4) Health Check

Set health check path to:

- /healthz

## 5) Deploy

1. Trigger deploy.
2. Wait for build and startup success.
3. Open service URL and verify:

- / loads landing page
- /pricing loads pricing page
- /healthz returns ok

## 6) Add Custom Domain (Optional)

1. In Render service, add custom domain.
2. Add DNS records as instructed by Render.
3. Wait for SSL provisioning.

## 7) Updating After Changes

Every push to main triggers auto deploy if Auto-Deploy is enabled.

## 8) When Backend Starts

1. Deploy API service separately (same Render account is fine).
2. Update Api__BaseUrl in this web service.
3. Redeploy the web service.

## 9) Full Stack Setup (Frontend + Backend)

If you want both frontend and backend running on Render, use two web services.

### Service A: Frontend (Blazor UI)

- Root Directory: src/EZPos.Web.Ui
- Purpose: website UI

### Service B: Backend (ASP.NET API)

- Root Directory: src/EZPos.Web.Api
- Purpose: API endpoints, payments, license logic

### Connect Them

1. Deploy backend service first and copy its public URL.
2. In frontend service environment variables, set:

- Api__BaseUrl = https://your-api-service.onrender.com

3. Redeploy frontend service.
