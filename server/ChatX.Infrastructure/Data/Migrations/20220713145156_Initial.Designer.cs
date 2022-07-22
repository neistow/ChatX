﻿// <auto-generated />
using System;
using ChatX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatX.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20220713145156_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatX.Domain.Conversation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserOneId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserTwoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserOneId", "UserTwoId")
                        .IsUnique();

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("ChatX.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("PreferredAges")
                        .HasColumnType("integer");

                    b.Property<int>("PreferredGenders")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SearchStartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SearchStartedAt");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
