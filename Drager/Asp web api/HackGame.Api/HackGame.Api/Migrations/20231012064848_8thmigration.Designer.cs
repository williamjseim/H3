﻿// <auto-generated />
using System;
using HackGame.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HackGame.Api.Migrations
{
    [DbContext(typeof(HackerGameDbContext))]
    [Migration("20231012064848_8thmigration")]
    partial class _8thmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HackGame.Api.Models.Database", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("IndexDescription")
                        .HasColumnType("longtext");

                    b.Property<int>("InternetKbs")
                        .HasColumnType("int");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Log")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Memory")
                        .HasColumnType("int");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("ProcessorGhz")
                        .HasColumnType("float");

                    b.Property<string>("Secret_Verifycation_Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StorageMb")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserIdFk")
                        .HasColumnType("char(36)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("VpnConnection")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("UserIdFk");

                    b.HasIndex("VpnConnection");

                    b.ToTable("Databases");
                });

            modelBuilder.Entity("HackGame.Api.Models.HackedDatabases", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("databaseId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("databaseId");

                    b.HasIndex("userId");

                    b.ToTable("Hacked_Databases");
                });

            modelBuilder.Entity("HackGame.Api.Models.Software", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DatabaseId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EncryptionStrength")
                        .HasColumnType("int");

                    b.Property<int>("HiddenStrength")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleteable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsInstalled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<bool>("StaticObject")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Strength")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UploadId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseId");

                    b.ToTable("Software_Data");
                });

            modelBuilder.Entity("HackGame.Api.Models.TimedTask", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("softwareId")
                        .HasColumnType("char(36)");

                    b.Property<string>("targetIp")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("softwareId");

                    b.HasIndex("userId");

                    b.ToTable("Timed_Tasks");
                });

            modelBuilder.Entity("HackGame.Api.Models.UserData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Login_Data");
                });

            modelBuilder.Entity("HackGame.Api.Models.UserInventoryData", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("MilitaryTechComps")
                        .HasColumnType("int");

                    b.Property<int>("highTechComps")
                        .HasColumnType("int");

                    b.Property<int>("microControllers")
                        .HasColumnType("int");

                    b.Property<int>("techComps")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Inventory_Data");
                });

            modelBuilder.Entity("HackGame.Api.Models.Database", b =>
                {
                    b.HasOne("HackGame.Api.Models.UserData", "User")
                        .WithMany()
                        .HasForeignKey("UserIdFk");

                    b.HasOne("HackGame.Api.Models.Database", "database")
                        .WithMany()
                        .HasForeignKey("VpnConnection");

                    b.Navigation("User");

                    b.Navigation("database");
                });

            modelBuilder.Entity("HackGame.Api.Models.HackedDatabases", b =>
                {
                    b.HasOne("HackGame.Api.Models.Database", "Database")
                        .WithMany()
                        .HasForeignKey("databaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackGame.Api.Models.UserData", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Database");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HackGame.Api.Models.Software", b =>
                {
                    b.HasOne("HackGame.Api.Models.Database", "data")
                        .WithMany()
                        .HasForeignKey("DatabaseId");

                    b.Navigation("data");
                });

            modelBuilder.Entity("HackGame.Api.Models.TimedTask", b =>
                {
                    b.HasOne("HackGame.Api.Models.Software", "software")
                        .WithMany()
                        .HasForeignKey("softwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackGame.Api.Models.UserData", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("software");

                    b.Navigation("user");
                });

            modelBuilder.Entity("HackGame.Api.Models.UserInventoryData", b =>
                {
                    b.HasOne("HackGame.Api.Models.UserData", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
