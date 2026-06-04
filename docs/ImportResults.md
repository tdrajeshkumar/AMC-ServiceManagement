# Import Results

Date: 2026-06-04 (UTC)
Workbook: `data-source/AMCRenewal-2025-2026_1852026.xlsx`

## Execution Summary

The application import flow was executed through startup with:

```bash
ASPNETCORE_ENVIRONMENT=Development timeout 90s mise exec -- dotnet run --project src/AMC.Web/AMC.Web.csproj
```

The import completed before the web host timeout. The command exited with timeout status after ASP.NET Core continued listening, not because the import failed.

## Workbook Processing

- sheets found: 24
- annual sheets detected: 3
- customer sheets detected: 21
- sheets successfully processed by current import flow:
  - `AMC 2026-2027`
  - `AMC 2025-2026 (2)`
- detected annual sheets with no persisted rows:
  - `AMC 2024-2025 `
- sheets skipped by current import flow:
  - 21 customer/item sheets are detected but not imported by the current annual-sheet import path.

## Database Counts After Import

SQLite database inspected: `src/AMC.Web/database/amc.db`

- Customer count: 33
- Contract count: 68
- Invoice count: 0
- BillingEvent count: 0
- PMVisit count: 0
- FollowUp count: 0

## Imported Contract Rows by Source Sheet

- `AMC 2025-2026 (2)`: 37 contracts
- `AMC 2026-2027`: 31 contracts

## Duplicate Handling Results

- Duplicate customers skipped/reported: 30
- Duplicate invoices skipped/reported: 0

## Rows Rejected / Validation Failures

Rejected records were written to `database/RejectedImportRows.json`.

- total rejected/validation records: 85
- duplicate customer validation records: 30
- invalid PM cycle records: 53
  - `AMC 2025-2026 (2)`: 27
  - `AMC 2026-2027`: 26
- invalid/uncertain contract period records: 2
  - `AMC 2025-2026 (2)`: 1
  - `AMC 2026-2027`: 1

## Errors

- No unhandled import exception after SQLite migration/table availability was corrected.

## Warnings

- `mise install` emitted transient HTTP retry warnings while resolving/downloading tool metadata.
- The ASP.NET Core run command was terminated by the configured `timeout` after import completion because the web host continued running.
- ASP.NET Core warned that `wwwroot` was not found; no static-file import behavior depends on this.
