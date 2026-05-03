// EZPos Site JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize Bootstrap tooltips and popovers
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
});

// Handle form submissions
function handleCheckout(event) {
    const form = event.target;
    if (form.checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
}

// Show loading state on button
function setButtonLoading(buttonElement, isLoading) {
    if (isLoading) {
        buttonElement.disabled = true;
        buttonElement.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Processing...';
    } else {
        buttonElement.disabled = false;
        buttonElement.innerHTML = 'Proceed to Payment';
    }
}

// Display error messages
function displayError(errorElement, message) {
    errorElement.textContent = message;
    errorElement.style.display = 'block';
}

// Clear error messages
function clearError(errorElement) {
    errorElement.textContent = '';
    errorElement.style.display = 'none';
}

console.log('EZPos License Management System Loaded');
