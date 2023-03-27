﻿// <auto-generated />
using System;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediatRAndRecordTypes.Api.Migrations
{
    [DbContext(typeof(MediatRAndRecordTypesDbContext))]
    [Migration("20230327072328_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediatRAndRecordTypes.Api.Models.Consult", b =>
                {
                    b.Property<Guid>("ConsultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConsultId");

                    b.ToTable("Consults");
                });

            modelBuilder.Entity("MediatRAndRecordTypes.Api.Models.Consult", b =>
                {
                    b.OwnsOne("MediatRAndRecordTypes.Api.Models.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<Guid>("ConsultId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2");

                            b1.HasKey("ConsultId");

                            b1.ToTable("Consults");

                            b1.WithOwner()
                                .HasForeignKey("ConsultId");
                        });

                    b.Navigation("DateRange")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
