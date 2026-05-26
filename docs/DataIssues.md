# Data Issues

## Critical Issues for Import
1. **Merged-cell dependent records**: contract/customer attributes are not repeated on every row, causing false nulls in raw extraction.
2. **Header drift across sheets**: different header rows and variant labels prevent static column indexing.
3. **Repeated ambiguous columns**: many `Amount`/`Remarks` columns without unique month keys.
4. **Cross-sheet heterogeneity**: annual billing sheets and equipment scope sheets are structurally different.
5. **Textual schedule semantics**: PM and payment cadence expressed in free text (`Quarterly advance`, `Half yearly post PM`).
6. **Spelling/name inconsistencies**: same entity likely appears under variant names.
7. **Date-format inconsistency**: period ranges, full dates, and month labels mixed.
8. **Potential duplicates**: invoice/reference values may recur across years or customers if not keyed by contract context.
9. **Ambiguous financial amount semantics**: repeated `Amount` labels may represent different concepts such as Contract Amount, Billing Amount, Received Amount, or PM-linked amount; label text alone must not determine meaning.

## Normalization/Validation Rules Needed
- Propagate merged values downward before validation.
- Canonicalize customer names (trim, collapse spaces, case-fold, punctuation fold).
- Canonicalize enumerations: PM cycle, payment terms, contract type.
- Parse date-like headers into normalized billing period columns.
- Preserve all free-text remarks in history table (do not overwrite latest only).
- Validate uniqueness on normalized customer + contract key and normalized invoice number.
