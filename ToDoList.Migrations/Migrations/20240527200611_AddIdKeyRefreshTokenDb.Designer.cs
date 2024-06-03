﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Core.DBContext;

#nullable disable

namespace ToDoList.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240527200611_AddIdKeyRefreshTokenDb")]
    partial class AddIdKeyRefreshTokenDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("ToDoList.Core.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("Create")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(828))
                        .HasColumnName("dateTimeCreate");

                    b.Property<DateTime?>("Expires")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValue(new DateTime(2024, 5, 28, 20, 6, 10, 954, DateTimeKind.Utc).AddTicks(495))
                        .HasColumnName("dateTimeExpires");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("refreshToken");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshTokens", (string)null);
                });

            modelBuilder.Entity("ToDoList.Core.Models.ToDoItem.ToDoItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime?>("DateCompletion")
                        .HasColumnType("TEXT")
                        .HasColumnName("dataCompletion");

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 23, 6, 10, 954, DateTimeKind.Local).AddTicks(5705))
                        .HasColumnName("dateCreate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsCaseCompletion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOLEAN")
                        .HasDefaultValue(false)
                        .HasColumnName("isCompletion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("toDoItems", (string)null);
                });

            modelBuilder.Entity("ToDoList.Core.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT")
                        .HasColumnName("passwordHash");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("User")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("ToDoList.Core.Models.RefreshToken", b =>
                {
                    b.HasOne("ToDoList.Core.Models.Users.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Core.Models.ToDoItem.ToDoItems", b =>
                {
                    b.HasOne("ToDoList.Core.Models.Users.User", "User")
                        .WithMany("ToDoItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Core.Models.Users.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}