﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workshop.Core.DataBase;

#nullable disable

namespace Workshop.Core.Migrations
{
    [DbContext(typeof(WorkshopDbContext))]
    [Migration("20220726084856_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Workshop.Core.Domains.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Workshop.Core.Entites.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("BoolValue")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeValue")
                        .HasColumnType("TEXT");

                    b.Property<double>("DoubleValue")
                        .HasColumnType("REAL");

                    b.Property<int>("IntValue")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IntWithFormula")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RowNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StringValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("StringWithNoteValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("StringWithStyleValue")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}