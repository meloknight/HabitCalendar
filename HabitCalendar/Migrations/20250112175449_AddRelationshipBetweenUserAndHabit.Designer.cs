﻿// <auto-generated />
using System;
using HabitCalendar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HabitCalendar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250112175449_AddRelationshipBetweenUserAndHabit")]
    partial class AddRelationshipBetweenUserAndHabit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HabitCalendar.Models.Habit", b =>
                {
                    b.Property<int>("HabitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HabitId"));

                    b.Property<string>("HabitDescription")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("HabitDisplayMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HabitName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("HabitId");

                    b.HasIndex("UserId");

                    b.ToTable("Habits");

                    b.HasData(
                        new
                        {
                            HabitId = 1,
                            HabitDescription = "Brush my teeth for 2 minutes",
                            HabitDisplayMethod = "icon",
                            HabitName = "Brush Teeth",
                            UserId = 1
                        },
                        new
                        {
                            HabitId = 2,
                            HabitDescription = "Workout for at least half an hour",
                            HabitDisplayMethod = "string",
                            HabitName = "Workout",
                            UserId = 2
                        },
                        new
                        {
                            HabitId = 3,
                            HabitDescription = "Take the dog out for a half hour walk",
                            HabitDisplayMethod = "icon",
                            HabitName = "Walk the dog",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("HabitCalendar.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Password = "1111",
                            StartDate = new DateOnly(2025, 1, 12),
                            UserName = "Billy123"
                        },
                        new
                        {
                            UserId = 2,
                            Password = "2222",
                            StartDate = new DateOnly(2025, 1, 12),
                            UserName = "Tommers5555"
                        });
                });

            modelBuilder.Entity("HabitCalendar.Models.Habit", b =>
                {
                    b.HasOne("HabitCalendar.Models.User", "User")
                        .WithMany("Habit")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HabitCalendar.Models.User", b =>
                {
                    b.Navigation("Habit");
                });
#pragma warning restore 612, 618
        }
    }
}
