﻿// <auto-generated />
using System;
using HWK6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HWK6.Migrations
{
    [DbContext(typeof(CoffeeShopDbContext))]
    partial class CoffeeShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HWK6.Models.Dlvy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DlvyName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Dlvy__3214EC27F447C463");

                    b.ToTable("Dlvy");
                });

            modelBuilder.Entity("HWK6.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("StationId")
                        .HasColumnType("int")
                        .HasColumnName("StationID");

                    b.HasKey("Id")
                        .HasName("PK__Item__3214EC2789BCEAFA");

                    b.HasIndex("StationId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("HWK6.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("DlvyId")
                        .HasColumnType("int")
                        .HasColumnName("DlvyID");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__Order__3214EC270CEBB029");

                    b.HasIndex("DlvyId");

                    b.HasIndex("StoreId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("HWK6.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Completed")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("ItemID");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int?>("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__OrderIte__3214EC27AAF36D16");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("HWK6.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StationName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Station__3214EC270425E54F");

                    b.ToTable("Station");
                });

            modelBuilder.Entity("HWK6.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StoreName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Store__3214EC27AFCE7937");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("HWK6.Models.Item", b =>
                {
                    b.HasOne("HWK6.Models.Station", "Station")
                        .WithMany("Items")
                        .HasForeignKey("StationId")
                        .IsRequired()
                        .HasConstraintName("Item_Fk_Station");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("HWK6.Models.Order", b =>
                {
                    b.HasOne("HWK6.Models.Dlvy", "Dlvy")
                        .WithMany("Orders")
                        .HasForeignKey("DlvyId")
                        .HasConstraintName("Order_Fk_Dlvy");

                    b.HasOne("HWK6.Models.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("Order_Fk_Store");

                    b.Navigation("Dlvy");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("HWK6.Models.OrderItem", b =>
                {
                    b.HasOne("HWK6.Models.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("OrderItem_Fk_Item");

                    b.HasOne("HWK6.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("OrderItem_Fk_Order");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("HWK6.Models.Dlvy", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HWK6.Models.Item", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("HWK6.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("HWK6.Models.Station", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("HWK6.Models.Store", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
