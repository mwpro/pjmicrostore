﻿// <auto-generated />
using System;
using Checkout.Orders.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Checkout.Orders.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20190807162124_AddShippingAndPayment")]
    partial class AddShippingAndPayment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Checkout.Orders.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Checkout.Orders.Domain.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("ProductPrice");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("Checkout.Orders.Domain.Order", b =>
                {
                    b.OwnsOne("Checkout.Orders.Domain.Customer", "Customer", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid?>("CustomerId");

                            b1.Property<string>("Email");

                            b1.Property<string>("Phone");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("Customer")
                                .HasForeignKey("Checkout.Orders.Domain.Customer", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Checkout.Orders.Domain.Delivery", "Delivery", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Fee");

                            b1.Property<string>("Name");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("Delivery")
                                .HasForeignKey("Checkout.Orders.Domain.Delivery", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Checkout.Orders.Domain.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Name");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("Status")
                                .HasForeignKey("Checkout.Orders.Domain.OrderStatus", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Checkout.Orders.Domain.Payment", "Payment", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Fee");

                            b1.Property<string>("Name");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("Payment")
                                .HasForeignKey("Checkout.Orders.Domain.Payment", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Checkout.Orders.Domain.CustomerAddress", "BillingAddress", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address");

                            b1.Property<string>("City");

                            b1.Property<string>("FirstName");

                            b1.Property<string>("LastName");

                            b1.Property<string>("Zip");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("BillingAddress")
                                .HasForeignKey("Checkout.Orders.Domain.CustomerAddress", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Checkout.Orders.Domain.CustomerAddress", "ShippingAddress", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address");

                            b1.Property<string>("City");

                            b1.Property<string>("FirstName");

                            b1.Property<string>("LastName");

                            b1.Property<string>("Zip");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.HasOne("Checkout.Orders.Domain.Order")
                                .WithOne("ShippingAddress")
                                .HasForeignKey("Checkout.Orders.Domain.CustomerAddress", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Checkout.Orders.Domain.OrderLine", b =>
                {
                    b.HasOne("Checkout.Orders.Domain.Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
