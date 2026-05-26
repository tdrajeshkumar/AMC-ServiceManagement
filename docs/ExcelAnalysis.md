# Excel Analysis

## Source Inspected
- Workbook found at `data-source/AMCRenewal-2025-2026_1852026.xlsx` (filename differs from requested path but clearly matches the renewal workbook).

## Worksheet Inventory
1. AMC 2026-2027
2. AMC 2025-2026 (2)
3. AMC 2024-2025
4. GADSEC
5. Ashok Leyland
6. DD Vaishanv College
7. Dailmer
8. Royal Enfield
9. KONE Elevator India P.Ltd
10. Enerji System Pvt Ltd
11. Padmapriya Hospital
12. Vlead Design
13. Dailmer(CAMC)
14. TAGROS ACMC
15. JOVE
16. Jawahar
17. GH INDUCTION
18. Global Healthcare
19. Hanon
20. Vikram Solar
21. GLOBAL 1007 & 0911
22. Tagros Dahej
23. Tagros Panoli
24. KEL_HEP 26

## Structural Findings
- The workbook is **multi-purpose**: three high-density annual planning sheets plus many customer-specific item/scope sheets.
- Major migration-relevant sheets are the annual tabs (`AMC 2026-2027`, `AMC 2025-2026 (2)`, `AMC 2024-2025`).
- Actual main planning headers detected include:
  - `S.NO`, `CLIENT`, `PRODUCT`, `TOTAL AMOUNT`, `Period`, `Duration`, `PM Cycle`, `PM Schedule`, `PM Month`, `Remarks`
  - Repeated monthly/periodic pairs: `Amount`, `Remarks` (many times across columns)
  - Summary tail columns: `TOTAL`, `Billed`, `Received amount`, `pending Amount`
- Customer-specific sheets mostly contain line-item **contract scope detail** columns such as:
  - `S No / Sr. No. / SNO`
  - `Item Description` variants
  - `Qty` variants
  - occasional `Warranty`, `Year of Installation`, split wing quantities, or periodized amount columns.

## Merged Cell Findings
- Heavy merged-cell usage in primary sheets:
  - `AMC 2026-2027`: 382 merged ranges
  - `AMC 2025-2026 (2)`: 417 merged ranges
  - `AMC 2024-2025`: 742 merged ranges
- Merges are used for visual grouping and repeated customer blocks, meaning row-level values are often present only in the first row of a block.

## Missing Value Findings
- Primary sheets have high apparent null rates if read row-by-row because merged blocks carry blank trailing rows.
- Repeated `Amount`/`Remarks` columns are sparsely populated depending on billing cadence.
- Summary fields (`TOTAL`, `Billed`, `Received amount`, `pending Amount`) are present only on certain rows.
- Several customer tabs have section-title rows inside data regions, producing blank item rows.

## Duplicate Pattern Findings
- Repeated customer/contract blocks exist intentionally across rows because one contract spans multiple PM/billing events.
- Header vocabulary duplicates with minor spelling/casing variations (`Remark` vs `Remarks`, `Descripton` typo).
- Multiple similarly named sheets may represent same account across years or contract variants (example: `Dailmer`, `Dailmer(CAMC)`).

## Formatting Inconsistencies
- Mixed header row positions (row 1, row 2, row 3, and later in some tabs).
- Inconsistent column naming (`Qty`, `QTY`, `Qty.`, `AMC REQUIRED EQUIPMENT QTY`).
- Mixed date formats in headers/content (`26.01.26`, `August 2026 to July 2027`, `Period`).
- Typographical inconsistencies (`TAGROS ACMC`, `Descripton`, `Dailmer`).
- Numeric values sometimes stored as text-like entries due to Excel formatting.
