﻿// <auto-generated />
using System;
using DringSpot.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DringSpot.DataAccess.EF.Migrations
{
    [DbContext(typeof(DringContext))]
    [Migration("20191126175836_ConfigureRelationship")]
    partial class ConfigureRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DringSpot.DataAccess.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.CategoryPlace", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingPlaceId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "MeetingPlaceId");

                    b.HasIndex("MeetingPlaceId");

                    b.ToTable("CategoryPlace");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.FavouredPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FavouredPlace");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.MeetingPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.PlaceWithinFindModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("PlacesWithin");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttendeeNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingPlaceId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingPlaceId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Votee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPositive")
                        .HasColumnType("bit");

                    b.Property<int?>("MeetingPlaceId")
                        .HasColumnType("int");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingPlaceId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Votee");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.CategoryPlace", b =>
                {
                    b.HasOne("DringSpot.DataAccess.Models.Category", "Category")
                        .WithMany("Places")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DringSpot.DataAccess.Models.MeetingPlace", "MeetingPlace")
                        .WithMany("Categories")
                        .HasForeignKey("MeetingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.FavouredPlace", b =>
                {
                    b.HasOne("DringSpot.DataAccess.Models.User", null)
                        .WithMany("FavouredPlaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Friend", b =>
                {
                    b.HasOne("DringSpot.DataAccess.Models.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Review", b =>
                {
                    b.HasOne("DringSpot.DataAccess.Models.MeetingPlace", null)
                        .WithMany("Reviews")
                        .HasForeignKey("MeetingPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DringSpot.DataAccess.Models.Votee", b =>
                {
                    b.HasOne("DringSpot.DataAccess.Models.MeetingPlace", null)
                        .WithMany("Votees")
                        .HasForeignKey("MeetingPlaceId");

                    b.HasOne("DringSpot.DataAccess.Models.Review", null)
                        .WithMany("Votees")
                        .HasForeignKey("ReviewId");
                });
#pragma warning restore 612, 618
        }
    }
}
