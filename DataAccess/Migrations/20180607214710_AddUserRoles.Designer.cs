﻿// <auto-generated />
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ScrumManagerDbContext))]
    [Migration("20180607214710_AddUserRoles")]
    partial class AddUserRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.RolesEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DataAccess.TeamEntity", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("ProductOwnerId");

                    b.Property<int?>("ProjectManagerId");

                    b.Property<int?>("ScrumMasterId");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("TeamId");

                    b.HasIndex("TeamName")
                        .IsUnique();

                    b.ToTable("Team");
                });

            modelBuilder.Entity("DataAccess.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int?>("RoleId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("DataAccess.XrefUserTeamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("TeamId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("XrefUserTeam");
                });

            modelBuilder.Entity("DataAccess.UserEntity", b =>
                {
                    b.HasOne("DataAccess.RolesEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("DataAccess.XrefUserTeamEntity", b =>
                {
                    b.HasOne("DataAccess.TeamEntity", "Team")
                        .WithMany("XrefUsersTeam")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.UserEntity", "User")
                        .WithMany("XrefUsersTeam")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
