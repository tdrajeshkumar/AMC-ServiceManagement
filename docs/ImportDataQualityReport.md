# Import Data Quality Report

Date: 2026-06-04 (UTC)
Workbook: `data-source/AMCRenewal-2025-2026_1852026.xlsx`

## Imported Data Summary

- customers imported: 33
- contracts imported: 68
- invoices imported: 0
- PM schedules imported: 0
- billing events imported: 0
- follow-ups imported: 0

## Customers Imported

- 33 distinct customer rows exist in SQLite after import.
- Duplicate customer handling reported 30 duplicate customer validation records.

## Contracts Imported

- 68 contract rows exist in SQLite after import.
- Imported contract rows came from two annual workbook sheets:
  - `AMC 2025-2026 (2)`: 37
  - `AMC 2026-2027`: 31

## Invoices Imported

- 0 invoice rows exist in SQLite after import.
- Duplicate invoice handling reported 0 duplicate invoice records.

## PM Schedules Imported

- 0 PMVisit rows exist in SQLite after import.
- 0 PMScheduleTemplate rows exist in SQLite after import.
- PM cycle validation produced 53 invalid PM cycle records in rejected-row output.

## Unresolved Mapping Issues

- 53 PM cycle values failed current validation.
- 2 contract period values were reported as invalid or uncertain.
- 30 duplicate customer values were reported by validation.

## Workbook Sections Not Yet Imported

- 21 detected customer/item sheets were not imported by the current annual-sheet import path.
- Billing staging rows are detected by the current import parser, but no BillingEvent rows are persisted by the current import services.
- PM staging rows are detected by the current import parser, but no PMVisit or PMScheduleTemplate rows are persisted by the current import services.
- Invoice rows are not persisted by the current import services.
