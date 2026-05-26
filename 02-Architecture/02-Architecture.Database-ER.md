# Database ER
Version: 1.0
Status: Initial Baseline

---

# Purpose

Define initial database entities and relationships for the AMC/CAMC Service Management System.

Design goals:

- Keep schema simple
- Avoid premature normalization
- Support Excel migration
- Support future growth
- Avoid unnecessary entities

---

# Entity Relationship Overview

Customer
    ↓
Contract
    ↓
Invoice
    ↓
FollowUp


Contract
    ↓
PMVisit
    ↓
PMReport


Engineer
    ↓
PMVisit


User
    ↓
Engineer

---

# Entity Diagram

+------------------+
| Customer         |
+------------------+
| CustomerId (PK)  |
+------------------+
        |
        |
        ▼

+------------------+
| Contract         |
+------------------+
| ContractId (PK)  |
| CustomerId (FK)  |
+------------------+
        |
        |
        +----------------+
        |                |
        ▼                ▼

+---------------+   +----------------+
| Invoice       |   | PMVisit        |
+---------------+   +----------------+
| InvoiceId(PK) |   | PMVisitId(PK)  |
| ContractIdFK  |   | ContractIdFK   |
+---------------+   | EngineerIdFK   |
                    +----------------+
                             |
                             |
                             ▼

                     +----------------+
                     | PMReport       |
                     +----------------+
                     | PMReportId(PK) |
                     | PMVisitIdFK    |
                     +----------------+

Invoice
    |
    ▼

+----------------+
| FollowUp       |
+----------------+
| FollowUpId(PK) |
| InvoiceIdFK    |
+----------------+

---

# Table Definitions

---

## Customer

Purpose:

Store customer master information.

Fields:

| Field | Type |
|---------|------|
| CustomerId | int |
| CustomerName | nvarchar(200) |
| ContactPerson | nvarchar(100) |
| Phone | nvarchar(50) |
| Email | nvarchar(100) |
| Address | nvarchar(max) |
| GSTNumber | nvarchar(50) |
| Notes | nvarchar(max) |
| IsActive | bit |

---

Relationship:

One Customer

↓

Many Contracts

---

## Contract

Purpose:

Store AMC/CAMC agreements.

Fields:

| Field | Type |
|---------|------|
| ContractId | int |
| CustomerId | int FK |
| ContractType | nvarchar(50) |
| ProductCovered | nvarchar(200) |
| ContractAmount | decimal(18,2) |
| StartDate | datetime |
| EndDate | datetime |
| BillingCycle | nvarchar(50) |
| PMCycle | nvarchar(50) |
| PaymentTerms | nvarchar(100) |
| PONumber | nvarchar(100) |
| Notes | nvarchar(max) |
| Status | nvarchar(50) |

---

Relationship:

One Contract

↓

Many Invoices

One Contract

↓

Many PM Visits

---

## Invoice

Purpose:

Store billing information.

Fields:

| Field | Type |
|---------|------|
| InvoiceId | int |
| ContractId | int FK |
| InvoiceNumber | nvarchar(100) |
| InvoiceDate | datetime |
| BillingPeriodFrom | datetime |
| BillingPeriodTo | datetime |
| InvoiceAmount | decimal(18,2) |
| ReceivedAmount | decimal(18,2) |
| DueDate | datetime |
| Status | nvarchar(50) |
| Remarks | nvarchar(max) |

---

Derived value:

PendingAmount

Formula:

PendingAmount =
InvoiceAmount − ReceivedAmount

Not stored physically.

Computed during query.

---

Relationship:

One Invoice

↓

Many FollowUps

---

## FollowUp

Purpose:

Track communication activities.

Fields:

| Field | Type |
|---------|------|
| FollowUpId | int |
| InvoiceId | int FK |
| FollowUpDate | datetime |
| ContactPerson | nvarchar(100) |
| Discussion | nvarchar(max) |
| NextFollowUpDate | datetime |
| Status | nvarchar(50) |

---

## Engineer

Purpose:

Store engineer information.

Fields:

| Field | Type |
|---------|------|
| EngineerId | int |
| Name | nvarchar(100) |
| Phone | nvarchar(50) |
| Email | nvarchar(100) |
| IsActive | bit |

---

Relationship:

One Engineer

↓

Many PM Visits

---

## PMVisit

Purpose:

Store PM schedules.

Fields:

| Field | Type |
|---------|------|
| PMVisitId | int |
| ContractId | int FK |
| EngineerId | int FK |
| VisitNumber | int |
| PlannedDate | datetime |
| ActualDate | datetime |
| Status | nvarchar(50) |
| Remarks | nvarchar(max) |

---

Status values:

- Scheduled
- Assigned
- Completed
- Missed
- Rescheduled

---

## PMReport

Purpose:

Store PM report files and details.

Fields:

| Field | Type |
|---------|------|
| PMReportId | int |
| PMVisitId | int FK |
| FileName | nvarchar(200) |
| FilePath | nvarchar(max) |
| UploadedDate | datetime |
| Summary | nvarchar(max) |

---
## AuditLog

Purpose:

Track changes to important business data.

Fields:

| Field | Type |
|---------|------|
| AuditLogId | bigint |
| TableName | nvarchar(100) |
| RecordId | nvarchar(100) |
| ActionType | nvarchar(50) |
| OldValue | nvarchar(max) |
| NewValue | nvarchar(max) |
| ChangedBy | nvarchar(100) |
| ChangedDate | datetime |
| IPAddress | nvarchar(100) |

ActionType:

- Create
- Update
- Delete
- StatusChange
---
# ASP.NET Identity Tables

Application authentication will use:

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetClaims

No custom user table required.

Engineer table may optionally reference:

AspNetUsers.Id

---

# Future Reserved Extensions

No implementation now.

Reserved only:

Contract
    ↓
PMVisit
    ↓
SpareUsage

Future modules:

- Spare tracking
- Warranty tracking
- Asset register
- Replacement history

---

# Index Recommendations

Create indexes:

Customer

- CustomerName

Contract

- CustomerId
- StartDate
- EndDate

Invoice

- ContractId
- InvoiceNumber
- InvoiceDate
- Status

PMVisit

- PlannedDate
- EngineerId
- Status

FollowUp

- NextFollowUpDate

---

# Database Rules

Avoid:

- duplicate customer records
- duplicate contract numbers
- duplicate invoice numbers

Prefer:

- soft delete (IsActive)
- FK constraints
- EF migrations
- nullable fields only where appropriate
