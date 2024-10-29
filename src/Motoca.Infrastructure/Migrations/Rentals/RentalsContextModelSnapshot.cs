﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motoca.Infrastructure.Rentals;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Motoca.Infrastructure.Migrations.Rentals
{
    [DbContext(typeof(RentalsContext))]
    partial class RentalsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Motoca.Domain.Rentals.AggregatesModel.PlanEntity", b =>
                {
                    b.Property<Guid>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("InternalId");

                    b.Property<short>("DurationTime")
                        .HasColumnType("smallint");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("PenaltyPercent")
                        .HasColumnType("decimal");

                    b.Property<decimal>("ValuePerDay")
                        .HasColumnType("decimal");

                    b.HasKey("EntityId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("rentals_plans");
                });

            modelBuilder.Entity("Motoca.Domain.Rentals.AggregatesModel.RentalEntity", b =>
                {
                    b.Property<Guid>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("InternalId");

                    b.Property<decimal>("AmountToPay")
                        .HasColumnType("decimal");

                    b.Property<string>("BikeId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("PlanEntityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RiderId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("EntityId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PlanEntityId");

                    b.ToTable("rentals");
                });

            modelBuilder.Entity("Motoca.Domain.Rentals.AggregatesModel.RentalEntity", b =>
                {
                    b.HasOne("Motoca.Domain.Rentals.AggregatesModel.PlanEntity", "Plan")
                        .WithMany("Rentals")
                        .HasForeignKey("PlanEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Motoca.Domain.Rentals.AggregatesModel.PlanEntity", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
