# Database Diagnostics

Date: 2026-05-27 (UTC)

## Scope

Diagnostics after replacing migration layer and applying SQLite migration.

## Commands Run

```bash
dotnet ef migrations add InitialCreate --project src/AMC.Web/AMC.Web.csproj --startup-project src/AMC.Web/AMC.Web.csproj
```

```bash
dotnet ef database update --project src/AMC.Web/AMC.Web.csproj --startup-project src/AMC.Web/AMC.Web.csproj
```

```bash
dotnet ef migrations list --project src/AMC.Web/AMC.Web.csproj --startup-project src/AMC.Web/AMC.Web.csproj
```

```bash
sqlite3 src/AMC.Web/database/amc.db ".tables"
```

## Migrations Found

- `20260527085049_InitialCreate`
- `dotnet ef migrations list` showed this migration as present.

## Tables Created (SQLite)

Actual tables listed by `sqlite3`:

- AuditLogs
- BillingEvents
- ContractScopeDetails
- Contracts
- Customers
- Engineers
- FollowUps
- InvoiceRemarkHistories
- Invoices
- PMReports
- PMScheduleTemplates
- PMVisits
- __EFMigrationsHistory
- sqlite_sequence

## DbContext Entity Sets Expected

From `ApplicationDbContext`:

- Customers
- Contracts
- BillingEvents
- Invoices
- FollowUps
- PMVisits
- PMReports
- Engineers
- ContractScopeDetails
- AuditLogs
- InvoiceRemarkHistories
- PMScheduleTemplates

## Verification Result

- SQLite now contains all primary tables expected by DbSet names (pluralized convention names).
- `Customers` table exists in `src/AMC.Web/database/amc.db`.

## Errors / Warnings Observed During Diagnostics

- Initial `dotnet ef database update` attempt failed with `SQLite Error 14: unable to open database file` until `src/AMC.Web/database/` directory existed.
- After creating `src/AMC.Web/database/`, migration applied successfully and tables were created.
- `mise` emitted transient HTTP retry warnings during tool metadata download.
