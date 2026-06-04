using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMC.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TableName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModuleName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    RecordId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FieldName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ActionType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    OldValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    ChangedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IPAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NormalizedCustomerName = table.Column<string>(type: "TEXT", nullable: true, computedColumnSql: "UPPER(TRIM(REPLACE([CustomerName], '.', '')))", stored: true),
                    ContactPerson = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    GSTNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Engineers",
                columns: table => new
                {
                    EngineerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineers", x => x.EngineerId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ProductCovered = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    ContractAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    BillingCycle = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PMCycle = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PMCycleOriginalText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PMScheduleText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PMMonthText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PaymentTerms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PaymentTermsOriginalText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PONumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ParentContractId = table.Column<int>(type: "INTEGER", nullable: true),
                    RenewalYear = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    RenewalReminderDays = table.Column<int>(type: "INTEGER", nullable: true),
                    SourceSheet = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SourceRowStart = table.Column<int>(type: "INTEGER", nullable: true),
                    SourceRowEnd = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contract_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Parent",
                        column: x => x.ParentContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingEvents",
                columns: table => new
                {
                    BillingEventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    BillingPeriodFrom = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    BillingPeriodTo = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    PlannedInvoiceDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    PlannedAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    GeneratedByRule = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingEvents", x => x.BillingEventId);
                    table.ForeignKey(
                        name: "FK_BillingEvent_Contract",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractScopeDetails",
                columns: table => new
                {
                    ContractScopeDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    WarrantyInfo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    InstallationYear = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SourceSection = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractScopeDetails", x => x.ContractScopeDetailId);
                    table.ForeignKey(
                        name: "FK_ContractScopeDetail_Contract",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMScheduleTemplates",
                columns: table => new
                {
                    PMScheduleTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    CycleCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CycleIntervalMonths = table.Column<int>(type: "INTEGER", nullable: true),
                    AdvanceOrPostPM = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    AnchorDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    OriginalRuleText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMScheduleTemplates", x => x.PMScheduleTemplateId);
                    table.ForeignKey(
                        name: "FK_PMScheduleTemplate_Contract",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMVisits",
                columns: table => new
                {
                    PMVisitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ScheduledDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CompletedDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    VisitRemarks = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduleSource = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SequenceNo = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMVisits", x => x.PMVisitId);
                    table.ForeignKey(
                        name: "FK_PMVisit_Contract",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PMVisit_Engineer",
                        column: x => x.EngineerId,
                        principalTable: "Engineers",
                        principalColumn: "EngineerId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillingEventId = table.Column<int>(type: "INTEGER", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    NormalizedInvoiceNumber = table.Column<string>(type: "TEXT", nullable: true, computedColumnSql: "NULLIF(UPPER(LTRIM(RTRIM([InvoiceNumber]))), '')", stored: true),
                    InvoiceDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    BillingPeriodFrom = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    BillingPeriodTo = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    ReceivedAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true, defaultValue: 0m),
                    DueDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_BillingEvents_BillingEventId",
                        column: x => x.BillingEventId,
                        principalTable: "BillingEvents",
                        principalColumn: "BillingEventId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PMReports",
                columns: table => new
                {
                    PMReportId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PMVisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FileSizeKB = table.Column<int>(type: "INTEGER", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMReports", x => x.PMReportId);
                    table.ForeignKey(
                        name: "FK_PMReport_PMVisit",
                        column: x => x.PMVisitId,
                        principalTable: "PMVisits",
                        principalColumn: "PMVisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    FollowUpDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    ContactPerson = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Discussion = table.Column<string>(type: "TEXT", nullable: true),
                    NextFollowUpDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_FollowUp_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRemarkHistories",
                columns: table => new
                {
                    InvoiceRemarkHistoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    RemarkText = table.Column<string>(type: "TEXT", nullable: false),
                    RemarkContext = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    RemarkDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    SourceSheet = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SourceRow = table.Column<int>(type: "INTEGER", nullable: true),
                    SourceColumn = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRemarkHistories", x => x.InvoiceRemarkHistoryId);
                    table.ForeignKey(
                        name: "FK_InvoiceRemarkHistory_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingEvents_ContractId",
                table: "BillingEvents",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ParentContractId",
                table: "Contracts",
                column: "ParentContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractScopeDetails_ContractId",
                table: "ContractScopeDetails",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "UX_Customer_NormalizedName_GST",
                table: "Customers",
                columns: new[] { "NormalizedCustomerName", "GSTNumber" },
                unique: true,
                filter: "[CustomerName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_InvoiceId",
                table: "FollowUps",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRemarkHistories_InvoiceId",
                table: "InvoiceRemarkHistories",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillingEventId",
                table: "Invoices",
                column: "BillingEventId");

            migrationBuilder.CreateIndex(
                name: "UX_Invoice_NormalizedInvoiceNumber",
                table: "Invoices",
                column: "NormalizedInvoiceNumber",
                unique: true,
                filter: "[NormalizedInvoiceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PMReports_PMVisitId",
                table: "PMReports",
                column: "PMVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_PMScheduleTemplates_ContractId",
                table: "PMScheduleTemplates",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PMVisits_ContractId",
                table: "PMVisits",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PMVisits_EngineerId",
                table: "PMVisits",
                column: "EngineerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ContractScopeDetails");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "InvoiceRemarkHistories");

            migrationBuilder.DropTable(
                name: "PMReports");

            migrationBuilder.DropTable(
                name: "PMScheduleTemplates");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PMVisits");

            migrationBuilder.DropTable(
                name: "BillingEvents");

            migrationBuilder.DropTable(
                name: "Engineers");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
