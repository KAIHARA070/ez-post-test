# Development Phases

This document outlines the phased development plan for the EZPos Web + License Key System. This modular approach allows for iterative development, focusing on delivering core functionality first and building upon it in subsequent phases.

---

## Phase 1: Core Foundation & License Validation

**Goal:** Establish the architectural backbone and implement the essential license validation mechanism required by the desktop application installer.

**Key Features:**

*   **Project Scaffolding:**
    *   [x] Create solution and project structure.
    *   [x] Define layered architecture (`Domain`, `Infra`, `App`, `Api`).
    *   [x] Set up project references.
*   **Database Setup:**
    *   [ ] Define `License` entity in the Domain layer.
    *   [ ] Configure EF Core with PostgreSQL in the Infrastructure layer.
    *   [ ] Create initial database migration.
*   **License Validation Endpoint:**
    *   [ ] Implement `POST /api/licenses/validate`.
    *   [ ] Create a service in the Application layer to check license key validity against the database.
*   **Basic Security:**
    *   [ ] Implement rate limiting on the validation endpoint.
*   **Documentation:**
    *   [x] Create initial architecture and design documents.

---

## Phase 2: Payment Integration & License Generation

**Goal:** Implement the complete payment-to-license workflow using Stripe.

**Key Features:**

*   **Stripe Integration:**
    *   [ ] Add Stripe .NET client library.
    *   [ ] Create a service to initiate Stripe Checkout sessions.
*   **Webhook Handling:**
    *   [ ] Implement the `POST /api/webhooks/stripe` endpoint.
    *   [ ] Add security to verify Stripe webhook signatures.
*   **License Generation:**
    *   [ ] Create a service to generate a unique license key.
    *   [ ] Implement the logic within the webhook handler to create and save a new `License` record after a successful payment.
*   **Customer Communication:**
    *   [ ] Integrate an email service (e.g., SendGrid) to send the license key to the customer.
*   **Front-End:**
    *   [ ] Create a simple landing page (Razor Page or static HTML) with a "Buy Now" button.
    *   [ ] Create purchase success and cancellation pages.

---

## Phase 3: Advanced Features & Administration

**Goal:** Enhance the system with subscription capabilities and administrative tools.

**Key Features:**

*   **Subscription Model:**
    *   [ ] Update the `License` model to better support recurring payments (e.g., `ExpirationDate`, `SubscriptionStatus`).
    *   [ ] Integrate Stripe Billing for subscription plans.
    *   [ ] Handle webhook events related to subscription lifecycle (`invoice.payment_succeeded`, `customer.subscription.deleted`, etc.).
*   **Customer Portal:**
    *   [ ] Implement a secure area for customers to view their license keys and manage their subscriptions (using Stripe's customer portal).
*   **Admin Dashboard:**
    *   [ ] Create a simple, protected web interface for administrators to:
        *   View sales and license data.
        *   Manually issue or revoke licenses.
        *   Troubleshoot customer issues.
*   **Analytics & Reporting:**
    *   [ ] Basic reporting on sales and activation trends.
