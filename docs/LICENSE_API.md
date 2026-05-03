# License API Specification

This document defines the API endpoints for license management.

## License Model

The core `License` entity represents a customer's right to use the software.

```csharp
// Location: src/EZPos.Web.Domain/License.cs

public class License
{
    public Guid Id { get; set; }
    public string LicenseKey { get; set; } // The unique, generated license key
    public string CustomerEmail { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpirationDate { get; set; } // For future subscription plans
    public bool IsActive { get; set; }
    public string StripeCheckoutSessionId { get; set; }
}
```

## API Endpoints

All endpoints are part of the ASP.NET Core Web API project.

---

### 1. Validate License

This endpoint is used by the Inno Setup installer to verify a license key during application setup.

*   **Endpoint:** `POST /api/licenses/validate`
*   **Request Body:**
    ```json
    {
      "licenseKey": "YOUR-LICENSE-KEY-HERE"
    }
    ```
*   **Success Response (200 OK):**
    ```json
    {
      "isValid": true,
      "message": "License is valid."
    }
    ```
*   **Failure Response (400 Bad Request or 404 Not Found):**
    ```json
    {
      "isValid": false,
      "message": "License is invalid or has expired."
    }
    ```

---

### 2. Stripe Webhook Handler

This is an internal endpoint that receives events from Stripe. It is critical for automating license generation after a payment.

*   **Endpoint:** `POST /api/webhooks/stripe`
*   **Request Body:** Stripe `Event` object (JSON). We are primarily interested in the `checkout.session.completed` event type.
*   **Response (200 OK):** An empty `200 OK` response is required to acknowledge receipt of the event to Stripe. Any other response will cause Stripe to retry sending the webhook.
*   **Security:** This endpoint **MUST** be protected. We will verify the request signature using a webhook signing secret provided by Stripe.

## Security Considerations

*   **Webhook Security:** The Stripe webhook endpoint is a public endpoint that triggers a critical business process (license generation). It must be secured by validating the `Stripe-Signature` header to ensure requests are genuinely from Stripe.
*   **Rate Limiting:** To prevent abuse of the validation endpoint, we will implement rate limiting based on IP address.
*   **Data Validation:** All incoming data (request bodies, webhook payloads) must be carefully validated to prevent injection attacks or unexpected errors.
