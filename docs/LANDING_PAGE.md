# Landing Page Plan (Fluent Style)

This plan defines the Home page foundation built with Blazor + Fluent UI components and a WinUI 3 inspired visual language.

## Page Composition

1. Header (sticky)
2. Hero block
3. Feature grid
4. Pricing preview cards
5. Final call-to-action strip
6. Footer

## Header

- Left: EZPos brand mark and product label.
- Center/right: navigation links (Home, Pricing).
- Right: dark/light mode switch (FluentSwitch).
- Surface style: translucent background blur with subtle border.

## Hero Block

- Bold headline focused on business outcomes.
- Supporting text with concise value proposition.
- Primary CTA: View Plans.
- Secondary CTA: Explore Features.
- Right-side visual panel: product screenshot placeholder in acrylic card.

Fluent style notes:
- Rounded panel corners (16px to 18px).
- Soft gradient glow behind hero panel.
- Micro-animations on entry and hover.

## Feature Grid

- 3 to 6 FluentCard elements.
- Each card includes icon, short title, and one-paragraph description.
- Content examples:
  - Fast checkout workflow
  - Inventory visibility
  - License security
  - Multi-branch readiness

## Pricing Preview

- One-time license card as primary offer.
- Optional placeholder for future subscription cards.
- Card includes:
  - Plan title
  - Price
  - Feature checklist
  - Buy button placeholder (no payment logic in this phase)

## CTA Strip

- Full-width highlighted panel near page end.
- Message: direct and action-oriented.
- Buttons:
  - Primary: Get EZPos License
  - Secondary: Contact Sales

## Footer

- Compact, business-oriented links.
- Legal placeholders for Terms and Privacy.
- Copyright text.

## Responsive Behavior

- Desktop: two-column hero and multi-column cards.
- Tablet: compressed hero and two-column cards.
- Mobile: single-column flow, sticky header retained, large tap targets.

## Animation Guidelines

- Section reveal: 180ms to 220ms fade+slide.
- Button interaction: 120ms scale/opacity transition.
- Keep animation subtle to maintain professional tone.
