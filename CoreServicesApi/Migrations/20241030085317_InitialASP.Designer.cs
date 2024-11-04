﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace CoreServicesApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241030085317_InitialASP")]
    partial class InitialASP
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cargo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("BillId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("itemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Bill", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Employee", b =>
                {
                    b.HasOne("Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.HasOne("Models.Bill", "Bill")
                        .WithMany("Items")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Bill", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
