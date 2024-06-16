using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.DAL.Models;

public partial class SportsDbContext : DbContext
{
    public SportsDbContext()
    {
    }

    public SportsDbContext(DbContextOptions<SportsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }

    public virtual DbSet<ProcurementDetail> ProcurementDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<SalesHistory> SalesHistories { get; set; }

    public virtual DbSet<SalesItem> SalesItems { get; set; }

    public virtual DbSet<SupplierDetail> SupplierDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SportsDB;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDetail>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8436FFFB3");

            entity.Property(e => e.CustomerContact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProcurementDetail>(entity =>
        {
            entity.HasKey(e => e.ProcurementId).HasName("PK__Procurem__95B451EC0770CCF0");

            entity.HasOne(d => d.Product).WithMany(p => p.ProcurementDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Procureme__Produ__45F365D3");

            entity.HasOne(d => d.Sup).WithMany(p => p.ProcurementDetails)
                .HasForeignKey(d => d.SupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Procureme__SupId__44FF419A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDF85BB240");

            entity.ToTable("Product");

            entity.Property(e => e.Price).HasDefaultValue(1);
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__46E78A0C");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProductC__19093A0B8BA9C9F0");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasKey(e => new { e.SupId, e.ProductId }).HasName("PK__ProductI__866349FA5ECDDC13");

            entity.ToTable("ProductInventory");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__48CFD27E");

            entity.HasOne(d => d.Sup).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.SupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__SupId__47DBAE45");
        });

        modelBuilder.Entity<SalesHistory>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__SalesHis__C952FB32F4552582");

            entity.ToTable("SalesHistory");

            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesHistories)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SalesHist__Custo__49C3F6B7");
        });

        modelBuilder.Entity<SalesItem>(entity =>
        {
            entity.HasKey(e => new { e.SalesId, e.CustomerId, e.ProductId }).HasName("PK__SalesIte__9FAC11B9AEF77F36");

            entity.ToTable("SalesItem");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesItems)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesItem__Custo__71D1E811");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesItem__Produ__70DDC3D8");

            entity.HasOne(d => d.Sup).WithMany(p => p.SalesItems)
                .HasForeignKey(d => d.SupId)
                .HasConstraintName("FK__SalesItem__SupId__6FE99F9F");
        });

        modelBuilder.Entity<SupplierDetail>(entity =>
        {
            entity.HasKey(e => e.SupId).HasName("PK__Supplier__4D238596567D0337");

            entity.Property(e => e.SubAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubContact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
