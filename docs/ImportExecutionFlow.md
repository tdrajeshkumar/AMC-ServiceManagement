# Import Execution Flow

Status Alignment: SCHEMA_FROZEN_V1, EF layer completed.

## Scope
This document defines only the Excel import infrastructure flow.

No controllers, views, APIs, or dashboards are part of this stage.

## Pipeline
Excel Reader (NPOI / NPOI.OOXML)

↓

Normalized Staging Objects

↓

Validation

↓

Entity Conversion

↓

Database Save

## Components
- `WorkbookAnalyzer`
  - Detects annual planning sheets vs customer item sheets.
  - Detects merged range counts.
  - Detects dynamic header rows.
- `ExcelImportService`
  - Orchestrates complete import execution.
  - Reads annual sheets and materializes staging rows.
  - Preserves original workbook remarks in staging rows.
- `ImportValidationService`
  - Validates duplicate customers.
  - Validates duplicate invoices (existing DB data).
  - Validates contract period plausibility.
  - Validates billing cycle and PM cycle values.
- `CustomerImportService`
  - Upserts customers from staging rows.
- `ContractImportService`
  - Converts validated staging contracts to entities and saves.
- `ImportResult`
  - Returns totals for rows, inserted, updated, skipped, errors, warnings.

## Workbook Rules Applied
- Detect annual planning sheets using name + header hints.
- Parse dynamic headers from detected header row.
- Carry forward customer name across merged/blank rows.
- Detect contract rows, PM rows, and billing rows from annual sheets.

## Summary Output
The importer returns:
- Total rows
- Inserted
- Updated
- Skipped
- Errors
- Warnings
- Diagnostic messages (including remark preservation counts)

## Remarks Preservation
Original workbook remarks are retained in staging objects (`RemarksRaw`) before entity conversion, enabling downstream history persistence logic without lossy parsing.
