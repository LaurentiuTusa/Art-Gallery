using System;
using System.Collections.Generic;
using Art_Gallery.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.DAL.DataContext;

public partial class ArtGalleryContext : DbContext
{
    public ArtGalleryContext()
    {
    }

    public ArtGalleryContext(DbContextOptions<ArtGalleryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductToCategory> ProductToCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_type");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("productType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_user");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(140)
                .IsUnicode(false)
                .HasColumnName("imgPath");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Order).WithMany(p => p.Products)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_product_order");
        });

        modelBuilder.Entity<ProductToCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_productToType");

            entity.ToTable("productToCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.TypeId).HasColumnName("typeId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductToCategories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productToType_product");

            entity.HasOne(d => d.Type).WithMany(p => p.ProductToCategories)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productToType_type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
