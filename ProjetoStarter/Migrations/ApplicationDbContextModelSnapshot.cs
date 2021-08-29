﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoStarter.Data;

namespace ProjetoStarter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjetoStarter.Models.Avaliacao", b =>
                {
                    b.Property<int>("AvaliacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comportamento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("Performance")
                        .HasColumnType("float");

                    b.Property<string>("Projeto")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("StarterId")
                        .HasColumnType("int");

                    b.HasKey("AvaliacaoId");

                    b.HasIndex("StarterId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("ProjetoStarter.Models.Starter", b =>
                {
                    b.Property<int>("StarterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Linguagem")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NomeStarter")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("StarterId");

                    b.ToTable("Starters");
                });

            modelBuilder.Entity("ProjetoStarter.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProjetoStarter.Models.Avaliacao", b =>
                {
                    b.HasOne("ProjetoStarter.Models.Starter", "Starter")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("StarterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
