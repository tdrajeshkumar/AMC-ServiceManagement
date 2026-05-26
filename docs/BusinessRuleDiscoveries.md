# Business Rule Discoveries

## Explicit/Implicit Rules Observed in Workbook
1. **Payment timing semantics are contractual logic**
   - Phrases like `Quarterly advance`, `Yearly advance`, `Half yearly post PM` indicate when invoice eligibility occurs relative to period/PM completion.
2. **PM cycle drives billing density**
   - Repeating `Amount/Remarks` columns correspond to cycle-specific billing/collection touchpoints rather than flat monthly invoicing for every contract.
3. **Remarks are operational history**
   - Remarks appear repeatedly across periods, indicating event-level notes (billing follow-up, collection status, exceptions) that must be preserved historically.
4. **Billed/Received/Pending are running financial states**
   - `pending Amount` is derivable and should not be source-of-truth editable without audit.
5. **Contract records are block-based**
   - One logical contract spans multiple physical rows through merged regions; importer must treat block as single parent with child events.
6. **Customer-specific sheets are contract scope annexures**
   - Customer-named tabs likely represent AMC/CAMC coverage items (equipment/assets covered under a contract) and should map to contract-scope detail, not customer master duplicates.
7. **Year tabs represent renewal snapshots**
   - Same client recurring across annual tabs implies renewal/version lineage rather than independent unrelated customers.

## Rule Codification Suggestions
- Invoice eligibility should be rule-driven and may depend on:
  - Billing cycle
  - PM completion
  - Advance/Post conditions
  - Contract start date
- Store `PaymentTermsOriginalText` and `PaymentTermsCode`.
- Store `PMCycleOriginalText` and `PMCycleCode`.
- Preserve all period remarks in separate history rows with event date/period context.
- Treat annual sheet row blocks as `Contract + BillingEvents + PMEvents`.
