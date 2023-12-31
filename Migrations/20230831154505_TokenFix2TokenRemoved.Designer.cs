﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SearchEngine.Data;

#nullable disable

namespace SearchEngine.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230831154505_TokenFix2TokenRemoved")]
    partial class TokenFix2TokenRemoved
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SearchEngine.Models.Counter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Entries")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Url");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("SearchEngine.Models.Page", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("Html")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("LastUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateOnly(2023, 8, 31));

                    b.Property<string>("SiteUrl")
                        .HasColumnType("text");

                    b.HasKey("Url");

                    b.HasIndex("SiteUrl");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("SearchEngine.Models.Site", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<DateOnly>("LastUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateOnly(2023, 8, 31));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Url");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SearchEngine.Models.Counter", b =>
                {
                    b.HasOne("SearchEngine.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("Url")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("SearchEngine.Models.Page", b =>
                {
                    b.HasOne("SearchEngine.Models.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteUrl");

                    b.Navigation("Site");
                });
#pragma warning restore 612, 618
        }
    }
}
