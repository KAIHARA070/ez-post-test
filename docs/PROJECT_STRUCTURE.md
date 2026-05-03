# Project Structure

This document outlines the folder structure for the EZPos Web + License Key System. The structure follows a clean, layered architecture to ensure separation of concerns and maintainability.

## Root Directory

```
/
|-- docs/                  # Documentation files
|-- src/                   # Source code
|   |-- EZPos.Web.Api/     # Presentation Layer (ASP.NET Core Web API)
|   |-- EZPos.Web.App/     # Business Logic Layer
|   |-- EZPos.Web.Infra/   # Data Access & Infrastructure Layer
|   |-- EZPos.Web.Domain/  # Core Domain Models
|-- .gitignore
|-- EZPos.Web.sln
|-- README.md
```

## Layer Explanations

### `src/EZPos.Web.Api` (Presentation Layer)

*   **Framework:** ASP.NET Core 8 Web API
*   **Responsibility:**
    *   Exposing API endpoints (Controllers or Minimal APIs).
    *   Handling HTTP requests and responses.
    *   Request validation and data transfer objects (DTOs).
    *   Authentication and authorization.
*   **Dependencies:** `EZPos.Web.App`

### `src/EZPos.Web.App` (Business Logic Layer)

*   **Framework:** .NET 8 Class Library
*   **Responsibility:**
    *   Implementing core business logic and use cases.
    *   Coordinating data flow between the Presentation and Data Access layers.
    *   Contains services for licensing, payments, and user management.
*   **Dependencies:** `EZPos.Web.Infra`, `EZPos.Web.Domain`

### `src/EZPos.Web.Infra` (Data Access & Infrastructure Layer)

*   **Framework:** .NET 8 Class Library
*   **Responsibility:**
    *   Data persistence using Entity Framework Core.
    *   Implementation of repository patterns for database access.
    *   Integration with external services like Stripe for payments.
    *   Database migrations.
*   **Dependencies:** `EZPos.Web.Domain`

### `src/EZPos.Web.Domain` (Core Domain Models)

*   **Framework:** .NET 8 Class Library
*   **Responsibility:**
    *   Contains the core domain entities (e.g., `License`, `Product`).
    *   Defines the shape of the data and its relationships.
    *   No dependencies on other layers.
