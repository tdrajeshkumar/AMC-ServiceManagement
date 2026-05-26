-- Initial schema derived from actual workbook structure + baseline ER
-- SQL Server dialect
-- Status: SCHEMA_FROZEN_V1

CREATE TABLE dbo.Customer (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(200) NOT NULL,
    NormalizedCustomerName AS UPPER(TRIM(REPLACE(CustomerName, '.', ''))) PERSISTED,
    ContactPerson NVARCHAR(100) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(MAX) NULL,
    GSTNumber NVARCHAR(50) NULL,
    Notes NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Customer_IsActive DEFAULT (1),
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL
);

CREATE UNIQUE INDEX UX_Customer_NormalizedName_GST
ON dbo.Customer (NormalizedCustomerName, GSTNumber)
WHERE CustomerName IS NOT NULL;

CREATE TABLE dbo.Contract (
    ContractId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    ContractType NVARCHAR(50) NULL,
    ProductCovered NVARCHAR(300) NULL,
    ContractAmount DECIMAL(18,2) NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    BillingCycle NVARCHAR(50) NULL,
    PMCycle NVARCHAR(50) NULL,
    PMCycleOriginalText NVARCHAR(200) NULL,
    PMScheduleText NVARCHAR(200) NULL,
    PMMonthText NVARCHAR(200) NULL,
    PaymentTerms NVARCHAR(100) NULL,
    PaymentTermsOriginalText NVARCHAR(200) NULL,
    PONumber NVARCHAR(100) NULL,
    Notes NVARCHAR(MAX) NULL,
    Status NVARCHAR(50) NULL,
    ParentContractId INT NULL,
    RenewalYear NVARCHAR(20) NULL,
    RenewalReminderDays INT NULL,
    SourceSheet NVARCHAR(100) NULL,
    SourceRowStart INT NULL,
    SourceRowEnd INT NULL,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL,
    CONSTRAINT FK_Contract_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(CustomerId),
    CONSTRAINT FK_Contract_Parent FOREIGN KEY (ParentContractId) REFERENCES dbo.Contract(ContractId)
);

CREATE TABLE dbo.Invoice (
    InvoiceId INT IDENTITY(1,1) PRIMARY KEY,
    BillingEventId INT NULL,
    InvoiceNumber NVARCHAR(100) NULL,
    NormalizedInvoiceNumber AS NULLIF(UPPER(LTRIM(RTRIM(InvoiceNumber))), '') PERSISTED,
    InvoiceDate DATE NULL,
    BillingPeriodFrom DATE NULL,
    BillingPeriodTo DATE NULL,
    InvoiceAmount DECIMAL(18,2) NULL,
    ReceivedAmount DECIMAL(18,2) NULL CONSTRAINT DF_Invoice_ReceivedAmount DEFAULT (0),
    DueDate DATE NULL,
    Status NVARCHAR(50) NULL,
    Remarks NVARCHAR(MAX) NULL,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL
);

CREATE UNIQUE INDEX UX_Invoice_NormalizedInvoiceNumber
ON dbo.Invoice (NormalizedInvoiceNumber)
WHERE NormalizedInvoiceNumber IS NOT NULL;

CREATE TABLE dbo.InvoiceRemarkHistory (
    InvoiceRemarkHistoryId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,
    RemarkText NVARCHAR(MAX) NOT NULL,
    RemarkContext NVARCHAR(100) NULL,
    RemarkDate DATE NULL,
    SourceSheet NVARCHAR(100) NULL,
    SourceRow INT NULL,
    SourceColumn NVARCHAR(20) NULL,
    CreatedOn DATETIME2 NOT NULL CONSTRAINT DF_InvoiceRemarkHistory_CreatedOn DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT FK_InvoiceRemarkHistory_Invoice FOREIGN KEY (InvoiceId) REFERENCES dbo.Invoice(InvoiceId)
);

CREATE TABLE dbo.BillingEvent (
    BillingEventId INT IDENTITY(1,1) PRIMARY KEY,
    ContractId INT NOT NULL,
    BillingPeriodFrom DATE NULL,
    BillingPeriodTo DATE NULL,
    PlannedInvoiceDate DATE NULL,
    PlannedAmount DECIMAL(18,2) NULL,
    Status NVARCHAR(50) NULL,
    GeneratedByRule NVARCHAR(100) NULL,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL,
    CONSTRAINT FK_BillingEvent_Contract FOREIGN KEY (ContractId) REFERENCES dbo.Contract(ContractId)
);

CREATE TABLE dbo.FollowUp (
    FollowUpId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,
    FollowUpDate DATE NULL,
    ContactPerson NVARCHAR(100) NULL,
    Discussion NVARCHAR(MAX) NULL,
    NextFollowUpDate DATE NULL,
    Status NVARCHAR(50) NULL,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL,
    CONSTRAINT FK_FollowUp_Invoice FOREIGN KEY (InvoiceId) REFERENCES dbo.Invoice(InvoiceId)
);

CREATE TABLE dbo.ContractScopeDetail (
    ContractScopeDetailId INT IDENTITY(1,1) PRIMARY KEY,
    ContractId INT NOT NULL,
    ItemDescription NVARCHAR(500) NULL,
    Quantity INT NULL,
    WarrantyInfo NVARCHAR(100) NULL,
    InstallationYear NVARCHAR(50) NULL,
    SerialNumber NVARCHAR(100) NULL,
    Category NVARCHAR(100) NULL,
    SourceSection NVARCHAR(100) NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_ContractScopeDetail_Contract FOREIGN KEY (ContractId) REFERENCES dbo.Contract(ContractId)
);

CREATE TABLE dbo.Engineer (
    EngineerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Engineer_IsActive DEFAULT (1)
);

CREATE TABLE dbo.PMVisit (
    PMVisitId INT IDENTITY(1,1) PRIMARY KEY,
    ContractId INT NOT NULL,
    EngineerId INT NULL,
    ScheduledDate DATE NULL,
    CompletedDate DATE NULL,
    Status NVARCHAR(50) NULL,
    VisitRemarks NVARCHAR(MAX) NULL,
    ScheduleSource NVARCHAR(50) NULL,
    SequenceNo INT NULL,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME2 NULL,
    CONSTRAINT FK_PMVisit_Contract FOREIGN KEY (ContractId) REFERENCES dbo.Contract(ContractId),
    CONSTRAINT FK_PMVisit_Engineer FOREIGN KEY (EngineerId) REFERENCES dbo.Engineer(EngineerId)
);

CREATE TABLE dbo.PMReport (
    PMReportId INT IDENTITY(1,1) PRIMARY KEY,
    PMVisitId INT NOT NULL,
    FileName NVARCHAR(200) NULL,
    FilePath NVARCHAR(MAX) NULL,
    ContentType NVARCHAR(100) NULL,
    FileSizeKB INT NULL,
    Summary NVARCHAR(MAX) NULL,
    UploadedDate DATETIME2 NULL,
    CONSTRAINT FK_PMReport_PMVisit FOREIGN KEY (PMVisitId) REFERENCES dbo.PMVisit(PMVisitId)
);

CREATE TABLE dbo.PMScheduleTemplate (
    PMScheduleTemplateId INT IDENTITY(1,1) PRIMARY KEY,
    ContractId INT NOT NULL,
    CycleCode NVARCHAR(50) NULL,
    CycleIntervalMonths INT NULL,
    AdvanceOrPostPM NVARCHAR(30) NULL,
    AnchorDate DATE NULL,
    OriginalRuleText NVARCHAR(200) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_PMScheduleTemplate_IsActive DEFAULT (1),
    CONSTRAINT FK_PMScheduleTemplate_Contract FOREIGN KEY (ContractId) REFERENCES dbo.Contract(ContractId)
);

CREATE TABLE dbo.AuditLog (
    AuditLogId BIGINT IDENTITY(1,1) PRIMARY KEY,
    TableName NVARCHAR(100) NULL,
    ModuleName NVARCHAR(100) NULL,
    RecordId NVARCHAR(100) NULL,
    FieldName NVARCHAR(100) NULL,
    ActionType NVARCHAR(50) NULL,
    OldValue NVARCHAR(MAX) NULL,
    NewValue NVARCHAR(MAX) NULL,
    ChangedBy NVARCHAR(100) NULL,
    ChangedDate DATETIME2 NULL,
    IPAddress NVARCHAR(100) NULL
);
