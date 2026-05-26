# Import Strategy

## Alignment with Approved Docs
- Uses staged import then validated transform flow from `03-DataMigration.ExcelToDb.Mapping.md`.
- Keeps schema simple and directly relational per `02-Architecture.Database-ER.md` and `02-Architecture.md`.
- Focuses strictly on analysis/design (no controller/view generation).

## Proposed Import Pipeline
1. **Workbook profiling**
   - Detect sheet type: `AnnualPlan` vs `ContractScopeDetail`.
2. **Excel Reader -> Normalized Staging Tables**
   - Read workbook rows directly and write into normalized staging tables (no cell-by-cell raw warehouse).
3. **Normalization pass**
   - Expand merged ranges to row-complete records.
   - Resolve header row dynamically per sheet.
   - Map variant header names to canonical fields.
4. **Staging entities**
   - `stg_contract_plan_row`
   - `stg_contract_billing_event`
   - `stg_contract_pm_event`
   - `stg_contract_item`
5. **Validation rules**
   - Required: customer, contract period/year context, total amount (when contract row).
   - Controlled vocabulary mapping for PM/payment terms.
   - Duplicate checks using normalized keys.
6. **Transform to production tables**
   - Upsert customer -> contract -> invoices/pm schedules.
   - Preserve raw remarks into history table.
7. **Import summary output**
   - Inserted/updated/skipped/error counts with row references.

## Canonical Execution Flow
Excel Reader
    ↓
Normalized Staging Tables
    ↓
Validation
    ↓
Production Tables

## Duplicate Prevention Strategy
- Customer dedupe key: `NormalizedCustomerName + GSTNumber(optional)`.
- Contract dedupe key: `CustomerId + ContractType + StartDate + EndDate + ProductCovered`.
- Invoice dedupe key: `NormalizedInvoiceNumber` (unique when present) else `(ContractId + BillingPeriodFrom + BillingPeriodTo)` fallback check.

## PM Scheduling Support
- Derive schedule seed from `PM Cycle`, `PM Schedule`, and periodized amount/month columns.
- Store both normalized cadence and original text.
- Generate future PM events from cadence template, not from historical rows only.
