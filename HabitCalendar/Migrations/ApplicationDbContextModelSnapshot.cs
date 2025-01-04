﻿// <auto-generated />
using HabitCalendar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HabitCalendar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HabitDisplayMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HabitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HabitId");

                    b.ToTable("Habits");

                    b.HasData(
                        new
                        {
                            HabitId = 1,
                            HabitDescription = "Brush my teeth for 2 minutes",
                            HabitDisplayMethod = "icon",
                            HabitName = "Brush Teeth"
                        },
                        new
                        {
                            HabitId = 2,
                            HabitDescription = "Workout for at least half an hour",
                            HabitDisplayMethod = "string",
                            HabitName = "Workout"
                        },
                        new
                        {
                            HabitId = 3,
                            HabitDescription = "Take the dog out for a half hour walk",
                            HabitDisplayMethod = "icon",
                            HabitName = "Walk the dog"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
