# UI Design Guidelines

This design system defines the visual and interaction baseline for EZPos web UI using Fluent Design principles inspired by WinUI 3.

## Design Goals

- Premium and native-like experience, not generic web styling.
- Dark mode first, with seamless light mode support.
- Minimal, calm, business-focused look for POS software users.
- Consistent spacing, rounded corners, and subtle elevation.

## Visual Language

- Surface style: Mica-like layered surfaces with translucent panels.
- Shape: Rounded corners from 10px to 18px depending on component level.
- Elevation: Soft, low-contrast shadows (no hard black shadows).
- Motion: Short, smooth transitions (120ms to 220ms) with ease-out timing.

## Color Tokens

- Accent primary: Blue (#3B82F6)
- Accent secondary: Teal (#14B8A6)
- Dark background: #0B1220
- Dark surface: #111A2B
- Dark border: rgba(255,255,255,0.10)
- Light background: #F3F6FB
- Light surface: #FFFFFF
- Text strong (dark mode): #EAF1FF
- Text strong (light mode): #13223A

## Typography

- Font family: Segoe UI Variable, Segoe UI, sans-serif.
- Heading weights: 600 to 700.
- Body weight: 400 to 500.
- Hero title size: clamp(2.2rem, 5vw, 4rem).
- Body size baseline: 1rem.

## Component Rules

- Use FluentButton for primary and secondary CTAs.
- Use FluentCard for feature and pricing modules.
- Use FluentSwitch for theme mode toggle.
- Keep icon usage minimal and functional.
- Avoid dense borders; rely on spacing and elevation.

## Layout Rules

- Maximum content width: 1200px.
- Global horizontal padding: 20px mobile, 32px desktop.
- Section spacing: 72px desktop, 48px mobile.
- Header remains sticky with translucent background blur.

## Accessibility Rules

- Minimum contrast: WCAG AA.
- Keyboard focus visible on all interactive controls.
- Touch targets at least 40px high.
- Motion reduced when prefers-reduced-motion is active.

## Theme Behavior

- Default mode: dark.
- Toggle mode using FluentSwitch in header.
- Persist mode in localStorage.
- Apply theme via root attribute: data-theme="dark|light".

## Expansion Guidance

- Reuse the same card and section patterns across Pricing, Dashboard, and Customer Portal.
- Add any new color first as a token, then consume it in component styles.
- Keep page-specific style files small and modular.