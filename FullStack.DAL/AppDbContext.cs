using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FullStack.DAL.Models
{
    public partial class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=LAPTOP-22S1FM0D\\SQLEXPRESS;Initial Catalog=AdventureWorks;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Sales");

                entity.HasIndex(e => e.AccountNumber)
                    .HasName("AK_Customer_AccountNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Customer_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.TerritoryId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TerritoryId).HasColumnName("TerritoryID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Person_BusinessEntityID");

                entity.ToTable("Person", "Person");

                entity.HasIndex(e => e.AdditionalContactInfo)
                    .HasName("PXML_Person_AddContact");

                entity.HasIndex(e => e.Demographics)
                    .HasName("XMLVALUE_Person_Demographics");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Person_rowguid")
                    .IsUnique();

                entity.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName });

                entity.Property(e => e.BusinessEntityId)
                    .HasColumnName("BusinessEntityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdditionalContactInfo).HasColumnType("xml");

                entity.Property(e => e.Demographics).HasColumnType("xml");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PersonType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasKey(e => e.SalesOrderId)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.ToTable("SalesOrderHeader", "Sales");

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_SalesOrderHeader_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.SalesOrderNumber)
                    .HasName("AK_SalesOrderHeader_SalesOrderNumber")
                    .IsUnique();

                entity.HasIndex(e => e.SalesPersonId);

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.AccountNumber).HasMaxLength(15);

                entity.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");

                entity.Property(e => e.Comment).HasMaxLength(128);

                entity.Property(e => e.CreditCardApprovalCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardId).HasColumnName("CreditCardID");

                entity.Property(e => e.CurrencyRateId).HasColumnName("CurrencyRateID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OnlineOrderFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchaseOrderNumber).HasMaxLength(25);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SalesOrderNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))");

                entity.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipMethodId).HasColumnName("ShipMethodID");

                entity.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TerritoryId).HasColumnName("TerritoryID");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeader)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
