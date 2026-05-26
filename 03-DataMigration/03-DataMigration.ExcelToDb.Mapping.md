# Excel To Database Mapping
Version: 1.0
Status: Initial Migration Baseline

---

# Purpose

Define mapping rules between the existing Excel source and the target database structure.

Source:

AMC Renewal details 2025-2026.xlsx

Goal:

Avoid manual re-entry and migrate existing operational data into normalized tables.

Migration flow:

Excel
    ↓
Import Utility
    ↓
Validation
    ↓
Transformation
    ↓
SQL Database

---

# Migration Principles

Approved:

✓ Import existing records where possible

✓ Preserve historical information

✓ Normalize repeated data

✓ Avoid duplicate customers

✓ Log invalid records

✓ Allow manual correction

---

Avoid:

✗ Direct import into production tables

✗ Blind insertion

✗ Duplicating customer names

✗ Discarding remarks/history

---

# Proposed Import Process

Step 1

Upload Excel file

↓

Step 2

Read rows using Excel import utility

↓

Step 3

Load into temporary staging tables

↓

Step 4

Validate data

↓

Step 5

Transform data

↓

Step 6

Insert into actual tables

↓

Step 7

Generate import summary

---

# Staging Tables

Temporary import tables:

ImportCustomerStage

ImportContractStage

ImportInvoiceStage

Purpose:

- isolate import failures
- validate data
- avoid corrupting production data

---

# Excel Column Mapping

## Customer Mapping

Excel Source

CLIENT

↓

Customer.CustomerName

---

Excel Source

CONTACT PERSON

↓

Customer.ContactPerson

---

Excel Source

PHONE

↓

Customer.Phone

---

Excel Source

ADDRESS

↓

Customer.Address

---

Excel Source

GST

↓

Customer.GSTNumber

---

# Contract Mapping

Excel Source

AMC/CAMC

↓

Contract.ContractType

Allowed values:

AMC

CAMC

---

Excel Source

START DATE

↓

Contract.StartDate

---

Excel Source

END DATE

↓

Contract.EndDate

---

Excel Source

TOTAL AMOUNT

↓

Contract.ContractAmount

---

Excel Source

PM CYCLE

↓

Contract.PMCycle

Allowed values:

Monthly

Quarterly

Half-Yearly

Yearly

---

Excel Source

PAYMENT TERMS

↓

Contract.PaymentTerms

Examples:

Yearly Advance

Quarterly Advance

Half Yearly Post PM

---

Excel Source

PO NUMBER

↓

Contract.PONumber

---

Excel Source

REMARKS

↓

Contract.Notes

---

# Invoice Mapping

Excel Source

INVOICE NUMBER

↓

Invoice.InvoiceNumber

---

Excel Source

INVOICE DATE

↓

Invoice.InvoiceDate

---

Excel Source

INVOICE AMOUNT

↓

Invoice.InvoiceAmount

---

Excel Source

PAYMENT RECEIVED

↓

Invoice.ReceivedAmount

---

Excel Source

REMARKS

↓

Invoice.Remarks

---

# PM Mapping

Excel Source

PM MONTH

↓

PMVisit.PlannedDate

Transformation required:

Month value

↓

Actual date generation

Rules:

Generate first-day placeholder initially:

Example:

May

↓

01-May-2026

Actual PM date may later be modified manually.

---

# Data Transformation Rules

---

## Rule 1

If customer already exists:

Do not create duplicate customer.

Matching criteria:

CustomerName

---

## Rule 2

If invoice number exists:

Skip insertion

Log duplicate

---

## Rule 3

Convert payment terms:

Example:

Excel:

Half Yearly Post PM

Database:

PaymentTerms="Post PM"

BillingCycle="Half-Yearly"

---

## Rule 4

Convert PM cycle:

Excel:

Qtrly

Database:

Quarterly

---

## Rule 5

Trim leading/trailing spaces

Example:

" Daimler "

↓

"Daimler"

---

## Rule 6

Convert invalid empty values:

Example:

NULL

Blank

"-"

↓

NULL

---

# Import Validation Rules

Customer Name:

Mandatory

---

Contract Start Date:

Mandatory

---

Contract End Date:

Mandatory

---

Contract Amount:

Must be numeric

---

Invoice Amount:

Must be numeric

---

Billing Cycle:

Must match:

- Monthly
- Quarterly
- Half-Yearly
- Yearly

---

PM Cycle:

Must match:

- Monthly
- Quarterly
- Half-Yearly
- Yearly

---

# Import Summary Report

After import generate:

Total Rows

Successful Rows

Failed Rows

Duplicate Customers

Duplicate Invoices

Warnings

Example:

Imported: 115

Success: 107

Failed: 8

Duplicates: 3

Warnings: 4

---

# Future Enhancements

Possible:

- Background import jobs
- Multiple Excel formats
- Mapping configuration screen
- Import history dashboard
