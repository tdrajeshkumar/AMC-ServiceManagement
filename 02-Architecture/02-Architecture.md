# Architecture
Version: 1.0
Status: Approved

---

# Architecture Goal

Maintain a lightweight and maintainable implementation suitable for a small-to-medium operational application.

The application shall intentionally avoid unnecessary enterprise abstractions.

Primary goals:

- Simplicity
- Maintainability
- Fast development
- Secure public deployment
- Easy onboarding
- Minimal project complexity

---

# Approved Technology Stack

## Backend

- ASP.NET Core MVC (.NET 8)

## ORM

- Entity Framework Core

## Database

- SQL Server

## Frontend

- Razor Views
- Bootstrap 5
- jQuery (minimal usage)

## Authentication

- ASP.NET Identity
- Cookie Authentication

## Logging

- Serilog

## File Storage

Initial:

Application File System

Future possibility:

Cloud Storage

---

# Architecture Principles

Approved:

✓ Single MVC project

✓ EF Core DbContext usage

✓ Lightweight helper/service classes

✓ Clear folder separation

✓ Minimal JavaScript

✓ Responsive UI

✓ Security-first approach

---

Avoid:

✗ Repository Pattern

✗ Separate Business Project

✗ Generic Repository

✗ Unit Of Work Pattern

✗ DTO-heavy architecture

✗ Mapping frameworks

✗ Microservice architecture

✗ Event bus patterns

✗ CQRS

Reason:

The application size and scope do not justify additional architectural complexity.

---

# High-Level Architecture

                Browser
                    │
                    │
                    ▼
          ASP.NET Core MVC
                    │
         ┌──────────┴──────────┐
         │                     │
         │                     │
    Controllers           Razor Views
         │
         │
         ▼
   Service Classes
         │
         ▼
    AppDbContext
         │
         ▼
      SQL Server

---

# Project Structure

AMC.Web

│

├── Controllers
│
│      DashboardController.cs
│      CustomerController.cs
│      ContractController.cs
│      InvoiceController.cs
│      PMController.cs
│      FollowUpController.cs
│
├── Models
│
│      Customer.cs
│      Contract.cs
│      Invoice.cs
│      PMVisit.cs
│      FollowUp.cs
│
├── ViewModels
│
│      DashboardVM.cs
│      InvoiceVM.cs
│      PMVisitVM.cs
│
├── Services
│
│      InvoiceSchedulerService.cs
│      PMService.cs
│      NotificationService.cs
│
├── Data
│
│      AppDbContext.cs
│      SeedData.cs
│
├── Helpers
│
│      DateHelper.cs
│      SecurityHelper.cs
│
├── Views
│
├── wwwroot
│
├── Program.cs
│
└── appsettings.json

---

# Service Layer Guidance

Service classes are helper classes only.

Service classes:

- should contain reusable business logic
- should not become large business modules
- should not contain database abstractions

Example:

Good:

InvoiceSchedulerService

Responsibilities:

- Generate billing dates
- Generate upcoming invoice events

Bad:

InvoiceMegaService.cs

Responsibilities:

- Invoice
- PM
- Authentication
- Dashboard
- Reporting
- Notifications

---

# Data Access Rules

Allowed:

```csharp
var invoices = await _dbContext.Invoices
                .Where(x=>x.Status=="Pending")
                .ToListAsync();
```

Allowed:

```csharp
Include()
AsNoTracking()
LINQ queries
```

Avoid:

```csharp
string sql =
"select * from Invoice where Name='"
+input;
```

Avoid:

```csharp
Stored procedures for basic CRUD
```

Stored procedures may be used later only for:

- reporting
- bulk import
- heavy calculations

---

# Authentication Architecture

Authentication:

ASP.NET Identity

Authorization:

Role Based

Roles:

Admin

Finance

ServiceLead

Engineer

Management

---

# Authorization Rules

UI visibility alone is not security.

Bad:

```csharp
@if(User.IsInRole("Admin"))
{
    ShowDeleteButton();
}
```

Good:

```csharp
[Authorize(Roles="Admin")]
public IActionResult Delete()
{
}
```

---

# Security Requirements

Public deployment requires mandatory hardening.

---

## HTTPS

Always enabled

```csharp
app.UseHttpsRedirection();
```

---

## Anti-forgery Protection

All POST requests:

```csharp
[ValidateAntiForgeryToken]
```

---

## Secure Cookies

```csharp
options.Cookie.HttpOnly=true;

options.Cookie.SecurePolicy=
CookieSecurePolicy.Always;

options.Cookie.SameSite=
SameSiteMode.Strict;
```

---

## Security Headers

Required:

Content-Security-Policy

X-Frame-Options

X-Content-Type-Options

Referrer-Policy

Strict-Transport-Security

---

## Login Protection

Enable lockout:

5 failed attempts

Lockout duration:

15 minutes

---

## File Upload Security

Allowed:

PDF

JPG

JPEG

PNG

Blocked:

EXE

DLL

BAT

JS

ZIP

Uploaded files should:

- validate extension
- validate content type
- use GUID naming
- store outside webroot

Example:

```text
b21d1c44_report.pdf
```

---

## Logging

Serilog shall capture:

- Login failures
- Exceptions
- Unauthorized attempts
- File uploads
- Important business events

---

# Deployment Assumptions

Hosting:

IIS

Environment:

Public HTTPS URL

Database:

SQL Server

Expected Scale:

Customers:

<500

Invoices annually:

<10000

PM visits annually:

<5000

Concurrent users:

<50

---

# Future Extension Guidelines

Future modules may be added without restructuring architecture.

Examples:

- Spare Management
- Warranty Tracking
- SMS Notifications
- WhatsApp Notifications
- Asset Tracking

Future additions should remain within the same MVC project unless significant scale requires redesign.
