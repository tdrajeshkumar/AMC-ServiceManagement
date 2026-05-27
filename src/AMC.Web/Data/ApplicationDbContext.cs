using AMC.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AMC.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<BillingEvent> BillingEvents => Set<BillingEvent>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<FollowUp> FollowUps => Set<FollowUp>();
    public DbSet<PMVisit> PMVisits => Set<PMVisit>();
    public DbSet<PMReport> PMReports => Set<PMReport>();
    public DbSet<Engineer> Engineers => Set<Engineer>();
    public DbSet<ContractScopeDetail> ContractScopeDetails => Set<ContractScopeDetail>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<InvoiceRemarkHistory> InvoiceRemarkHistories => Set<InvoiceRemarkHistory>();
    public DbSet<PMScheduleTemplate> PMScheduleTemplates => Set<PMScheduleTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .Property(x => x.NormalizedCustomerName)
            .HasComputedColumnSql("UPPER(TRIM(REPLACE([CustomerName], '.', '')))", stored: true);

        modelBuilder.Entity<Customer>()
            .HasIndex(x => new { x.NormalizedCustomerName, x.GSTNumber })
            .HasDatabaseName("UX_Customer_NormalizedName_GST")
            .IsUnique()
            .HasFilter("[CustomerName] IS NOT NULL");

        modelBuilder.Entity<Invoice>()
            .Property(x => x.NormalizedInvoiceNumber)
            .HasComputedColumnSql("NULLIF(UPPER(LTRIM(RTRIM([InvoiceNumber]))), '')", stored: true);

        modelBuilder.Entity<Invoice>()
            .HasIndex(x => x.NormalizedInvoiceNumber)
            .HasDatabaseName("UX_Invoice_NormalizedInvoiceNumber")
            .IsUnique()
            .HasFilter("[NormalizedInvoiceNumber] IS NOT NULL");

        modelBuilder.Entity<Contract>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Contracts)
            .HasForeignKey(x => x.CustomerId)
            .HasConstraintName("FK_Contract_Customer")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(x => x.ParentContract)
            .WithMany(x => x.ChildContracts)
            .HasForeignKey(x => x.ParentContractId)
            .HasConstraintName("FK_Contract_Parent")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BillingEvent>()
            .HasOne(x => x.Contract)
            .WithMany(x => x.BillingEvents)
            .HasForeignKey(x => x.ContractId)
            .HasConstraintName("FK_BillingEvent_Contract")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.BillingEvent)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.BillingEventId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<FollowUp>()
            .HasOne(x => x.Invoice)
            .WithMany(x => x.FollowUps)
            .HasForeignKey(x => x.InvoiceId)
            .HasConstraintName("FK_FollowUp_Invoice")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<InvoiceRemarkHistory>()
            .HasOne(x => x.Invoice)
            .WithMany(x => x.InvoiceRemarkHistories)
            .HasForeignKey(x => x.InvoiceId)
            .HasConstraintName("FK_InvoiceRemarkHistory_Invoice")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PMVisit>()
            .HasOne(x => x.Contract)
            .WithMany(x => x.PMVisits)
            .HasForeignKey(x => x.ContractId)
            .HasConstraintName("FK_PMVisit_Contract");

        modelBuilder.Entity<PMVisit>()
            .HasOne(x => x.Engineer)
            .WithMany(x => x.PMVisits)
            .HasForeignKey(x => x.EngineerId)
            .HasConstraintName("FK_PMVisit_Engineer")
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<PMReport>()
            .HasOne(x => x.PMVisit)
            .WithMany(x => x.PMReports)
            .HasForeignKey(x => x.PMVisitId)
            .HasConstraintName("FK_PMReport_PMVisit")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ContractScopeDetail>()
            .HasOne(x => x.Contract)
            .WithMany(x => x.ContractScopeDetails)
            .HasForeignKey(x => x.ContractId)
            .HasConstraintName("FK_ContractScopeDetail_Contract");

        modelBuilder.Entity<PMScheduleTemplate>()
            .HasOne(x => x.Contract)
            .WithMany(x => x.PMScheduleTemplates)
            .HasForeignKey(x => x.ContractId)
            .HasConstraintName("FK_PMScheduleTemplate_Contract");

        modelBuilder.Entity<Invoice>().Property(x => x.ReceivedAmount).HasDefaultValue(0m);
        modelBuilder.Entity<Customer>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<Engineer>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<PMScheduleTemplate>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<InvoiceRemarkHistory>().Property(x => x.CreatedOn).HasDefaultValueSql("SYSUTCDATETIME()");
    }
}
