using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMC.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText("database/InitialSchema.sql"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AuditLog", schema: "dbo");
            migrationBuilder.DropTable(name: "ContractScopeDetail", schema: "dbo");
            migrationBuilder.DropTable(name: "FollowUp", schema: "dbo");
            migrationBuilder.DropTable(name: "InvoiceRemarkHistory", schema: "dbo");
            migrationBuilder.DropTable(name: "PMReport", schema: "dbo");
            migrationBuilder.DropTable(name: "PMScheduleTemplate", schema: "dbo");
            migrationBuilder.DropTable(name: "Invoice", schema: "dbo");
            migrationBuilder.DropTable(name: "PMVisit", schema: "dbo");
            migrationBuilder.DropTable(name: "BillingEvent", schema: "dbo");
            migrationBuilder.DropTable(name: "Engineer", schema: "dbo");
            migrationBuilder.DropTable(name: "Contract", schema: "dbo");
            migrationBuilder.DropTable(name: "Customer", schema: "dbo");
        }
    }
}
