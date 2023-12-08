using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HWK6.Models;

public partial class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext()
    {
    }

    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dlvy> Dlvies { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLazyLoadingProxies()        // <-- add this line
                .UseSqlServer("Name=CoffeeShopConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dlvy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dlvy__3214EC27F447C463");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3214EC2789BCEAFA");

            entity.HasOne(d => d.Station).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_Fk_Station");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC270CEBB029");

            entity.HasOne(d => d.Dlvy).WithMany(p => p.Orders).HasConstraintName("Order_Fk_Dlvy");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("Order_Fk_Store");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC27AAF36D16");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasConstraintName("OrderItem_Fk_Item");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("OrderItem_Fk_Order");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC270425E54F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC27AFCE7937");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
