﻿// <auto-generated />
using System;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(PetExchangeDbContext))]
    partial class PetExchangeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessLayer.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BusinessLayer.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "age");

                    b.Property<string>("AnimalType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "animal_type");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<bool>("IncludesCage")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "includes_cage");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "photo_path");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Pets");

                    b.HasAnnotation("Relational:JsonPropertyName", "pet");
                });

            modelBuilder.Entity("BusinessLayer.Models.PublicOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "is_resolved");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("PublicOffers");
                });

            modelBuilder.Entity("BusinessLayer.Models.Town", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Towns");

                    b.HasAnnotation("Relational:JsonPropertyName", "town");
                });

            modelBuilder.Entity("BusinessLayer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "contact_info");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "is_admin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "photo_path");

                    b.Property<Guid>("TownId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "username");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("Users");

                    b.HasAnnotation("Relational:JsonPropertyName", "user");
                });

            modelBuilder.Entity("BusinessLayer.Models.UserRequests", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "is_accepted");

                    b.Property<Guid>("PublicOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicOfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");

                    b.HasAnnotation("Relational:JsonPropertyName", "requests");
                });

            modelBuilder.Entity("BusinessLayer.Models.Pet", b =>
                {
                    b.HasOne("BusinessLayer.Models.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessLayer.Models.PublicOffer", b =>
                {
                    b.HasOne("BusinessLayer.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("BusinessLayer.Models.Town", b =>
                {
                    b.HasOne("BusinessLayer.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BusinessLayer.Models.User", b =>
                {
                    b.HasOne("BusinessLayer.Models.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Town");
                });

            modelBuilder.Entity("BusinessLayer.Models.UserRequests", b =>
                {
                    b.HasOne("BusinessLayer.Models.PublicOffer", "PublicOffer")
                        .WithMany()
                        .HasForeignKey("PublicOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLayer.Models.User", null)
                        .WithMany("Requests")
                        .HasForeignKey("UserId");

                    b.Navigation("PublicOffer");
                });

            modelBuilder.Entity("BusinessLayer.Models.User", b =>
                {
                    b.Navigation("Pets");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}