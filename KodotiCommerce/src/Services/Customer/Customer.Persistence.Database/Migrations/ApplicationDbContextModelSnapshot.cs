﻿// <auto-generated />
using Customer.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Customer.Persistence.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Customer")
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Customer.Domain.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            Name = "Client 1"
                        },
                        new
                        {
                            ClientId = 2,
                            Name = "Client 2"
                        },
                        new
                        {
                            ClientId = 3,
                            Name = "Client 3"
                        },
                        new
                        {
                            ClientId = 4,
                            Name = "Client 4"
                        },
                        new
                        {
                            ClientId = 5,
                            Name = "Client 5"
                        },
                        new
                        {
                            ClientId = 6,
                            Name = "Client 6"
                        },
                        new
                        {
                            ClientId = 7,
                            Name = "Client 7"
                        },
                        new
                        {
                            ClientId = 8,
                            Name = "Client 8"
                        },
                        new
                        {
                            ClientId = 9,
                            Name = "Client 9"
                        },
                        new
                        {
                            ClientId = 10,
                            Name = "Client 10"
                        },
                        new
                        {
                            ClientId = 11,
                            Name = "Client 11"
                        },
                        new
                        {
                            ClientId = 12,
                            Name = "Client 12"
                        },
                        new
                        {
                            ClientId = 13,
                            Name = "Client 13"
                        },
                        new
                        {
                            ClientId = 14,
                            Name = "Client 14"
                        },
                        new
                        {
                            ClientId = 15,
                            Name = "Client 15"
                        },
                        new
                        {
                            ClientId = 16,
                            Name = "Client 16"
                        },
                        new
                        {
                            ClientId = 17,
                            Name = "Client 17"
                        },
                        new
                        {
                            ClientId = 18,
                            Name = "Client 18"
                        },
                        new
                        {
                            ClientId = 19,
                            Name = "Client 19"
                        },
                        new
                        {
                            ClientId = 20,
                            Name = "Client 20"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}