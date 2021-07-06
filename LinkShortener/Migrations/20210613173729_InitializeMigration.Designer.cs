﻿// <auto-generated />
using System;
using LinkShortener.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LinkShortener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210613173729_InitializeMigration")]
    partial class InitializeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LinkShortener.Data.Link.Link", b =>
                {
                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MainLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortLink")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("LinkShortener.Data.Statics.Statics", b =>
                {
                    b.Property<DateTime>("CreateTimeTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpV4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortLink")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Statics");
                });
#pragma warning restore 612, 618
        }
    }
}
