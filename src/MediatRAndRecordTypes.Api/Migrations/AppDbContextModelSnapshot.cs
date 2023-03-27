// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace MediatRAndRecordTypes.Api.Migrations;

[DbContext(typeof(MediatRAndRecordTypesDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseIdentityColumns()
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("ProductVersion", "5.0.0");

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

                b.Navigation("DateRange");
            });
    }
}

