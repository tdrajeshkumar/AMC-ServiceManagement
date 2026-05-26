# UI Guidelines
Version: 1.0
Status: Mandatory

---

# Purpose

Define UI implementation rules for the AMC/CAMC Service Management System.

Primary goals:

- Dense information display
- Maximize work area
- Reduce wasted space
- Fast navigation
- Consistent screen layout
- Minimize visual noise

This application is an operational business tool.

It is NOT a marketing website.

Avoid decorative UI patterns that reduce usable space.

---

# Design Philosophy

Priority order:

1. Data visibility
2. Fast actions
3. Reduced clicks
4. Dense but readable layout
5. Consistency
6. Appearance

---

# General Layout Rules

Use:

✓ Fixed top navigation

✓ Left collapsible menu

✓ Full-width content area

✓ Compact filters

✓ Compact tables

✓ Minimal headers

Avoid:

✗ Card inside card

✗ Large hero sections

✗ Large banners

✗ Dashboard-style empty spaces

✗ Large margins

✗ Large paddings

✗ Decorative graphics

---

# Screen Layout Structure

Preferred:

-------------------------------------------------

Top Nav

-------------------------------------------------

Left Menu | Page Toolbar

            -----------------------

            Filters

            -----------------------

            Data Grid

            -----------------------

            Pagination

-------------------------------------------------

Avoid:

-------------------------------------------------

Large Heading Card

Large Summary Card

Large Filter Card

Large Data Card

-------------------------------------------------

---

# Page Header Rules

Bad:

+------------------------------------------------+
| CUSTOMER MANAGEMENT                            |
| Manage customer lifecycle and customer records |
|                                                |
+------------------------------------------------+

Good:

Customer Management
---------------------------------------

Actions:

[Add Customer]

[Import]

[Export]

---

Rules:

Heading:

font-size: 18px

font-weight: 600

Margin bottom:

10px

No card container

No subtitle paragraphs unless necessary

---

# Spacing Rules

Default spacing:

Margins:

8px–12px

Padding:

8px–12px

Grid gaps:

8px

Avoid:

24px

32px

48px

padding unless absolutely required

---

# Filter Layout Rules

Preferred:

-------------------------------------------------

Customer [______]

Status [▼]

Date [______]

[Search]

[Clear]

-------------------------------------------------

Rules:

✓ Horizontal filters

✓ One-row layout

✓ Compact controls

Avoid:

✗ Vertical filter cards

✗ Huge filter sections

---

# Table Rules

Tables are primary work areas.

Use:

✓ Sticky headers

✓ Pagination

✓ Sorting

✓ Search

✓ Row highlighting

✓ Compact rows

✓ Status badges

Recommended:

Row height:

36px–42px

Avoid:

60px–80px rows

---

# Dashboard Rules

Dashboard should show:

Financial

Service

Renewals

Cards:

Small KPI cards only

Example:

-------------------------------------------------

Revenue

₹12.5L

Pending

₹3.1L

PM Due

8

Renewals

5

-------------------------------------------------

Rules:

✓ Small KPI cards

✓ Maximum 4–6 cards per row

Avoid:

✗ giant dashboard cards

✗ charts occupying full screen

✗ image banners

---

# Typography Rules

Font:

Segoe UI

Fallback:

Arial,sans-serif

Heading:

18px

Subheading:

15px

Normal text:

13px

Grid text:

12px–13px

Avoid:

✗ large fonts

✗ mixed font families

---

# Icon Rules

Use:

Bootstrap icons

Only where useful

Examples:

Edit

Delete

Upload

Download

Avoid:

✗ excessive icons

✗ decorative icons

---

# CSS Organization Rules

Structure:

wwwroot/

    css/

        site.css

        layout.css

        grid.css

        forms.css

        dashboard.css

        utilities.css

---

Avoid:

Huge site.css

Example:

Bad:

site.css

8000+ lines

Good:

Separate concerns

---

# JavaScript Organization Rules

Structure:

wwwroot/

    js/

        common.js

        customer.js

        invoice.js

        pm.js

        dashboard.js

---

Rules:

common.js

Shared functions only

Page-specific scripts remain separate

Avoid:

✗ giant app.js

✗ inline scripts in views

---

# jQuery Rules

Allowed:

✓ AJAX

✓ form validation

✓ modal handling

✓ dynamic grids

Avoid:

✗ large business logic

✗ DOM-heavy manipulation

✗ multiple nested event chains

---

# Images

Avoid:

✗ stock photos

✗ decorative illustrations

✗ dashboard graphics

Allowed:

✓ company logo

✓ report attachments

✓ PM photos

---

# Responsive Rules

Desktop:

Primary design target

Tablet:

Supported

Mobile:

Mandatory only for:

- Engineer Portal
- PM Visits
- PM Reports

---

# Final Rule

Usable work area is more important than visual decoration.

Every screen should maximize visible operational data.
