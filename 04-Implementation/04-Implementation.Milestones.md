# Milestones
Version: 1.0
Status: Approved Implementation Sequence

---

# Purpose

Define implementation order and delivery boundaries for the AMC/CAMC Service Management System.

Goals:

- Avoid parallel uncontrolled development
- Reduce implementation risk
- Keep features independently testable
- Deliver usable functionality early
- Prevent architecture drift

---

# Implementation Principles

Approved:

✓ Build vertically

✓ Complete one module before starting another

✓ Database first

✓ Functional screens before optimization

✓ Working software preferred over excessive abstraction

✓ Deliver usable increments

Avoid:

✗ Building all database tables first without UI

✗ Building all screens first without backend

✗ Premature optimization

✗ Multi-module parallel development

---

# Development Sequence

Stage 1

Foundation Setup

↓

Stage 2

Excel Analysis and Data Discovery

↓

Stage 3

Database and Import Design

↓

Stage 4

Core Business Modules

↓

Stage 5

Service Operations

↓

Stage 6

Field Operations

↓

Stage 7

Stabilization

---

# Milestone 1

Name:

Project Foundation

Estimated Outcome:

Application bootstrapped and deployable

Deliverables:

Project Setup

- ASP.NET Core MVC (.NET 8)
- EF Core
- SQL Server
- ASP.NET Identity
- Serilog

Configuration

- Environment setup
- appsettings structure
- connection strings
- dependency registration

Security

- HTTPS
- cookie security
- antiforgery
- security headers
- login lockout

Database

Create initial tables:

- Customer
- Contract
- Invoice
- FollowUp
- Engineer
- PMVisit
- PMReport

Acceptance Criteria:

✓ Application builds

✓ Application runs

✓ Authentication works

✓ Database migration executes

✓ Login/logout works

---
# Milestone 2

Name:

Excel Analysis and Data Discovery

Purpose:

Analyze actual Excel structure before implementation.

Deliverables:

Tasks:

- Inspect uploaded workbook
- Identify sheets
- Identify actual columns
- Detect merged cells
- Detect formatting inconsistencies
- Detect duplicate customer records
- Detect invoice patterns
- Detect PM schedule patterns
- Detect payment terminology

Generate:

- Excel structure summary
- Identified business rules
- Transformation assumptions
- Data quality issues

Acceptance Criteria:

✓ Workbook analyzed

✓ Column mappings identified

✓ Business rule assumptions documented

✓ Data issues documented
----

# Milestone 3

Name:

Database and Import Design

Purpose:

Align schema with actual source data.

Deliverables:

- SQL schema
- EF entities
- Staging tables
- Import utility
- Validation rules
- Seed strategy

Acceptance Criteria:

✓ Schema generated

✓ Staging tables created

✓ Import process defined

✓ Seed strategy approved

---
# Milestone 4

Name:

Customer Management

Deliverables:

Screens:

- Customer List
- Customer Create
- Customer Edit
- Customer Details

Features:

- Search customer
- Add customer
- Edit customer
- Disable customer

Acceptance Criteria:

✓ Customer CRUD works

✓ Validation works

✓ Duplicate prevention works

---

# Milestone 5

Name:

Contract Management

Deliverables:

Screens:

- Contract List
- Contract Create
- Contract Edit
- Contract Details

Features:

- Add contract
- Edit contract
- Contract search
- Expiry status

Acceptance Criteria:

✓ Contract CRUD works

✓ Contract linked to customer

✓ Validation works

---

# Milestone 6

Name:

Invoice Management

Deliverables:

Screens:

- Invoice List
- Invoice Create
- Invoice Edit

Features:

- Invoice tracking
- Payment recording
- Pending amount calculation

Acceptance Criteria:

✓ Invoice CRUD works

✓ Payment tracking works

✓ Pending calculation works

---

# Milestone 7

Name:

Billing Planner

Deliverables:

Features:

- Auto billing generation
- Upcoming billing list
- Dashboard widgets

Business Rules:

Yearly:

Generate:

1 billing event

Quarterly:

Generate:

4 billing events

Half-Yearly:

Generate:

2 billing events

Acceptance Criteria:

✓ Billing events generated correctly

✓ Dashboard displays upcoming billing

---

# Milestone 8

Name:

Excel Import

Deliverables:

Screens:

- Upload Import File
- Import Summary

Features:

- Read Excel
- Staging validation
- Duplicate checking
- Error reporting

Acceptance Criteria:

✓ Excel import works

✓ Duplicate records prevented

✓ Import summary generated

---

# Milestone 9

Name:

Dashboard

Deliverables:

Widgets:

Financial

- Revenue Expected
- Pending Invoice
- Overdue Invoice

Service

- PM Due
- PM Overdue

Renewal

- Expiring Contracts

Acceptance Criteria:

✓ Dashboard loads correctly

✓ KPIs calculated correctly

---

# Phase 1 Completion

Expected Deliverables:

✓ Customer Management

✓ Contract Management

✓ Invoice Management

✓ Billing Planner

✓ Dashboard

✓ Excel Import

Result:

System becomes operational for finance activities.

---

# Milestone 10

Name:

PM Scheduler

Deliverables:

Screens:

- PM Calendar
- PM List
- PM Details

Features:

- Schedule PM
- Assign engineer
- Reschedule

Acceptance Criteria:

✓ PM scheduling works

✓ Assignment works

---

# Milestone 11

Name:

Follow-up Tracker

Deliverables:

Features:

- Follow-up creation
- Next follow-up tracking

Acceptance Criteria:

✓ Follow-up lifecycle works

---

# Phase 2 Completion

Expected Deliverables:

✓ PM Scheduler

✓ Follow-up Tracker

✓ Alerts

Result:

Service operations become manageable.

---

# Milestone 12

Name:

Engineer Portal

Deliverables:

Features:

- Today's visits
- Pending visits
- Complete visit

Acceptance Criteria:

✓ Engineers can manage visits

---

# Milestone 13

Name:

PM Reports

Deliverables:

Features:

- Upload PM report
- Upload photos
- Customer signoff

Acceptance Criteria:

✓ Reports upload correctly

✓ Files validated

---

# Phase 3 Completion

Expected Deliverables:

✓ Engineer Portal

✓ PM Reports

Result:

Field execution becomes integrated.

---

# Deferred Milestones

Do not implement:

- Spare Tracking
- Asset Management
- Warranty Management
- Replacement Tracking

Status:

Deferred pending business discussion

---

# Final Rule

Milestones must be implemented sequentially.

Do not start future milestones before current milestone acceptance.
