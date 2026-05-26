# Codex Master Instructions
Version: 1.0
Status: Source Of Truth

---

# Read First

Before generating any implementation:

Read ALL uploaded documents completely.

Required documents:

01-Discovery/
    Project.Discovery.md

02-Architecture/
    Architecture.md
    Database-ER.md

03-DataMigration/
    ExcelToDb.Mapping.md
    ScreenInventory.md

Use these documents as the authoritative implementation source.

Do not invent architecture.

Do not add additional patterns unless explicitly approved.

---

# Project Overview

Project:

AMC/CAMC Service Management System

Purpose:

Manage:

- Customer information
- AMC/CAMC contracts
- Invoice tracking
- Billing planning
- PM scheduling
- Follow-up tracking
- Engineer activities
- PM reports

---

# Technology Requirements

Backend:

ASP.NET Core MVC (.NET 8)

Database:

SQL Server

ORM:

Entity Framework Core

Frontend:

Razor Views
Bootstrap 5

Authentication:

ASP.NET Identity

Logging:

Serilog

Hosting:

IIS

---

# Architecture Rules

Mandatory:

✓ Single MVC project

✓ Use EF Core DbContext directly

✓ Use lightweight service/helper classes

✓ Maintain folder separation

✓ Responsive UI

✓ Security-first implementation

---

Forbidden:

✗ Repository pattern

✗ Separate business project

✗ Generic repository

✗ Unit Of Work

✗ CQRS

✗ Event Bus

✗ Microservices

✗ Mapping frameworks

✗ DTO-heavy architecture

Reason:

Keep implementation simple and maintainable.

---

# Project Structure

AMC.Web

│

├── Controllers

├── Models

├── ViewModels

├── Services

├── Data

├── Helpers

├── Views

├── wwwroot

├── Program.cs

└── appsettings.json

---

# Controller Rules

Controllers should:

✓ remain small

✓ validate input

✓ call helper/service classes

✓ avoid large business logic

Bad:

CustomerController.cs

1500+ lines

Good:

CustomerController

- CRUD operations

PMController

- PM scheduling

InvoiceController

- Invoice activities

---

# Data Access Rules

Allowed:

```csharp
var invoices =
await _dbContext.Invoices
.Where(x=>x.Status=="Pending")
.ToListAsync();
```

Allowed:

- Include()
- AsNoTracking()
- LINQ

Avoid:

```csharp
string sql=
"select * from Invoice where Name='"
+input;
```

Avoid:

Stored procedures for simple CRUD.

Stored procedures allowed later only for:

- reporting
- bulk import
- heavy processing

---

# Authentication Rules

Use:

ASP.NET Identity

Roles:

Admin

Finance

ServiceLead

Engineer

Management

---

# Authorization Rules

Never rely on UI hiding.

Bad:

```csharp
@if(User.IsInRole("Admin"))
{
}
```

Good:

```csharp
[Authorize(Roles="Admin")]
```

---

# Security Rules

Mandatory for public deployment.

---

## HTTPS

Always enabled

```csharp
app.UseHttpsRedirection();
```

---

## Anti-forgery

All POST actions:

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

Implement:

- Content-Security-Policy
- X-Frame-Options
- X-Content-Type-Options
- Referrer-Policy
- Strict-Transport-Security

---

## Login Lockout

Failed attempts:

5

Lockout:

15 minutes

---

## File Upload Rules

Allowed:

- PDF
- JPG
- JPEG
- PNG

Blocked:

- EXE
- DLL
- BAT
- JS
- ZIP

Files must:

- validate extension
- validate MIME type
- rename with GUID
- store outside wwwroot

Example:

```
b32a4f_report.pdf
```

---


# Audit Requirements

Mandatory:

Create lightweight audit trail support.

Audit:

Customer

Contract

Invoice

PMVisit

FollowUp

Capture:

- Action type
- Old values
- New values
- User
- Timestamp
- IP address

Implementation:

Use EF Core SaveChanges interceptor.

Do not use third-party auditing frameworks.

One more business rule I would add:

Audit these fields even if only one value changes:

Financial

Contract Amount
Invoice Amount
Payment Received

Dates

Contract Start Date
Contract End Date
PM Planned Date
Invoice Date

Business Information

Payment Terms
PO Number
Product Covered
Customer Name

Status

Paid
Overdue
Completed
Cancelled
---
# Phase Rules

Build only approved phases.

---

Phase 1

Build:

✓ Dashboard

✓ Customer Master

✓ Contract Master

✓ Invoice Tracker

✓ Billing Planner

✓ Excel Import

Do NOT build:

- PM module
- Follow-up module
- Engineer portal
- Spare tracking

---

Phase 2

Build:

✓ PM Scheduler

✓ Alerts

✓ Follow-up Tracker

---

Phase 3

Build:

✓ Engineer Portal

✓ PM Report Upload

---

# Deferred Scope

Do NOT implement:

- Spare tracking
- Warranty tracking
- Asset register
- Replacement history

Only reserve extension capability.

---

# UI Requirements

Use:

✓ Bootstrap cards

✓ Filterable grids

✓ Pagination

✓ Responsive layouts

✓ Validation messages

Avoid:

✗ SPA frameworks

✗ excessive JavaScript

✗ deep menu structures

---

# Existing Data Migration

Source:

AMC Renewal details 2025-2026.xlsx

Requirements:

- Create import utility
- Create staging tables
- Validate rows
- Prevent duplicates
- Generate import summary

---

# Final Instruction

Follow uploaded documents strictly.

If implementation conflicts with uploaded documents:

Uploaded documents take precedence.

Do not redesign architecture.

Do not introduce complexity.
