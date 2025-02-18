﻿// <auto-generated />
using System;
using Axpo.Challenge.FullStack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Axpo.Challenge.FullStack.Migrations
{
    [DbContext(typeof(EnergyBalanceDbContext))]
    [Migration("20241017123743_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.BalancingCircle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BalancingCircles");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.ForecastData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Forecast")
                        .HasColumnType("float");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Forecasts");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BalancingCircleId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProducer")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BalancingCircleId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.TimeseriesEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("TimeseriesEntries");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.ForecastData", b =>
                {
                    b.HasOne("Axpo.Challenge.FullStack.Models.Domain.Member", "Member")
                        .WithMany("Forecasts")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.Member", b =>
                {
                    b.HasOne("Axpo.Challenge.FullStack.Models.Domain.BalancingCircle", "BalancingCircle")
                        .WithMany("Members")
                        .HasForeignKey("BalancingCircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BalancingCircle");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.TimeseriesEntry", b =>
                {
                    b.HasOne("Axpo.Challenge.FullStack.Models.Domain.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.BalancingCircle", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Axpo.Challenge.FullStack.Models.Domain.Member", b =>
                {
                    b.Navigation("Forecasts");
                });
#pragma warning restore 612, 618
        }
    }
}
