# Screen Inventory
Version: 1.0
Status: Approved Baseline

---

# Purpose

Defines the UI screens to be generated for the AMC/CAMC Service Management System.

Purpose:

- Provide a UI contract for implementation
- Prevent random screen generation
- Maintain consistent navigation
- Support stakeholder discussions
- Provide implementation guidance for Codex

---

# Navigation Structure

Dashboard
│
├── Customer Management
│
├── Contract Management
│
├── Invoice Management
│
├── PM Scheduler
│
├── Follow-up Tracker
│
├── Engineer Portal
│
└── Administration

---

# Screen 1

Name:

Dashboard

Route:

/Dashboard

Purpose:

Provide operational summary.

Components:

Financial Cards

- Revenue Expected This Month
- Pending Invoices
- Overdue Invoices
- Payments Received

Service Cards

- PM Due Today
- PM Overdue
- Engineer Workload

Renewal Cards

- Expiring in 30 days
- Expiring in 60 days

Tables:

Upcoming Billings

Columns:

- Customer
- Invoice Date
- Amount
- Status

Upcoming PM Visits

Columns:

- Customer
- PM Date
- Engineer
- Status

Actions:

- View Details
- Create Invoice
- Schedule PM

---

# Screen 2

Name:

Customer List

Route:

/Customer

Purpose:

Display customers.

Components:

Search

Filters

- Active
- Inactive

Grid:

Columns:

- Customer Name
- Contact Person
- Phone
- Contract Count
- Status

Actions:

- Add Customer
- Edit
- View
- Disable

---

# Screen 3

Name:

Customer Create/Edit

Route:

/Customer/Create

Purpose:

Maintain customer data.

Fields:

- Customer Name
- Contact Person
- Phone
- Email
- Address
- GST Number
- Notes

Actions:

- Save
- Cancel

---

# Screen 4

Name:

Contract List

Route:

/Contract

Purpose:

Display contract records.

Filters:

- AMC/CAMC
- Active
- Expiring
- Customer

Grid:

Columns:

- Customer
- Contract Type
- Start Date
- End Date
- Amount
- Billing Cycle
- PM Cycle
- Status

Actions:

- Add Contract
- Edit
- View

---

# Screen 5

Name:

Contract Create/Edit

Route:

/Contract/Create

Purpose:

Maintain contract information.

Fields:

- Customer
- Contract Type
- Product Covered
- Contract Amount
- Start Date
- End Date
- Billing Cycle
- PM Cycle
- Payment Terms
- PO Number
- Notes

Actions:

- Save
- Cancel

---

# Screen 6

Name:

Invoice Tracker

Route:

/Invoice

Purpose:

Track invoices.

Filters:

- Customer
- Status
- Month
- Year

Grid:

Columns:

- Invoice Number
- Customer
- Invoice Date
- Invoice Amount
- Received Amount
- Pending Amount
- Due Date
- Status

Actions:

- Add Invoice
- Mark Payment Received
- View Invoice History

---

# Screen 7

Name:

Invoice Create/Edit

Route:

/Invoice/Create

Fields:

- Contract
- Invoice Number
- Invoice Date
- Billing Period From
- Billing Period To
- Amount
- Due Date
- Remarks

Actions:

- Save
- Cancel

---

# Screen 8

Name:

PM Scheduler

Route:

/PM

Purpose:

Manage PM visits.

Views:

Calendar View

List View

Filters:

- Engineer
- Month
- Customer
- Status

Grid:

Columns:

- Customer
- Planned Date
- Engineer
- Status

Actions:

- Schedule PM
- Assign Engineer
- Reschedule

---

# Screen 9

Name:

PM Visit Details

Route:

/PM/Details

Fields:

- Customer
- Contract
- Engineer
- Planned Date
- Actual Date
- Status
- Remarks

Actions:

- Complete PM
- Upload PM Report

---

# Screen 10

Name:

Follow-up Tracker

Route:

/FollowUp

Purpose:

Track customer communications.

Grid:

Columns:

- Customer
- Invoice Number
- Follow-up Date
- Contact Person
- Next Follow-up Date
- Status

Actions:

- Add Follow-up
- Edit

---

# Screen 11

Name:

Engineer Portal

Route:

/Engineer

Purpose:

Restricted operational portal.

Cards:

- Today's Visits
- Pending Visits
- Completed Visits

Grid:

Columns:

- Customer
- Visit Date
- Status

Actions:

- Start Visit
- Complete Visit
- Upload Report

---

# Screen 12

Name:

PM Report Upload

Route:

/PMReport

Purpose:

Upload PM reports.

Fields:

- PM Visit
- Upload File
- Summary
- Remarks

Allowed Files:

- PDF
- JPG
- JPEG
- PNG

Actions:

- Upload
- Save

---

# Screen 13

Name:

User Management

Route:

/Admin/Users

Purpose:

Manage system users.

Grid:

Columns:

- Username
- Role
- Status

Actions:

- Add User
- Edit User
- Disable User

---

# Responsive Requirements

Desktop:

Primary layout

---

Tablet:

Supported

---

Mobile:

Supported for:

- Engineer Portal
- PM Visits
- PM Reports

---

# UI Guidelines

Use:

✓ Bootstrap 5

✓ Consistent card layouts

✓ Tables with filters

✓ Modal dialogs where useful

✓ Pagination

✓ Validation messages

Avoid:

✗ Heavy JavaScript frameworks

✗ Complex SPA architecture

✗ Excessive popup usage

✗ Deep nested navigation

---

# Future Reserved Screens

Not for current implementation:

- Spare Tracking
- Warranty Management
- Asset Register
- Replacement History
