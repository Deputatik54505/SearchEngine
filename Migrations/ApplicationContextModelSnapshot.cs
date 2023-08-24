﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SearchEngine.Data;

#nullable disable

namespace SearchEngine.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SearchEngine.Data.Models.Page", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<DateOnly>("LastUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateOnly(2023, 8, 24));

                    b.Property<string>("SiteUrl")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Url");

                    b.HasIndex("SiteUrl");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("SearchEngine.Data.Models.Site", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("LastUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateOnly(2023, 8, 24));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TokenType")
                        .HasColumnType("text");

                    b.HasKey("Url");

                    b.HasIndex("TokenType");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SearchEngine.Data.Models.Token", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Type");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("SearchEngine.Data.Models.Page", b =>
                {
                    b.HasOne("SearchEngine.Data.Models.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteUrl");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SearchEngine.Data.Models.Site", b =>
                {
                    b.HasOne("SearchEngine.Data.Models.Token", null)
                        .WithMany("Urls")
                        .HasForeignKey("TokenType");
                });

            modelBuilder.Entity("SearchEngine.Data.Models.Token", b =>
                {
                    b.Navigation("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}