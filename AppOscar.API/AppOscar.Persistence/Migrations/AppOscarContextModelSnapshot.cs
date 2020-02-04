﻿// <auto-generated />
using System;
using AppOscar.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppOscar.Persistence.Migrations
{
    [DbContext(typeof(AppOscarContext))]
    partial class AppOscarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("AppOscar.Models.Categoria", b =>
                {
                    b.Property<Guid>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoriaPhotoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FilmeVencedorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PontosCategoria")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdCategoria");

                    b.HasIndex("FilmeVencedorId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("AppOscar.Models.Filme", b =>
                {
                    b.Property<Guid>("IdFilme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilmePhotoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeFilme")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdFilme");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("AppOscar.Models.Participacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("IdCategoria")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdFilme")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdFilme");

                    b.ToTable("Participacoes");
                });

            modelBuilder.Entity("AppOscar.Models.User", b =>
                {
                    b.Property<int>("id_user")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("emailUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("nomeUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("senhaUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id_user");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AppOscar.Models.Voto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("DthCriacao")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdParticipacao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdParticipacao");

                    b.ToTable("Votos");
                });

            modelBuilder.Entity("AppOscar.Models.Categoria", b =>
                {
                    b.HasOne("AppOscar.Models.Filme", "FilmeVencedor")
                        .WithMany("CategoriasVencidas")
                        .HasForeignKey("FilmeVencedorId");
                });

            modelBuilder.Entity("AppOscar.Models.Participacao", b =>
                {
                    b.HasOne("AppOscar.Models.Categoria", "Categoria")
                        .WithMany("Participantes")
                        .HasForeignKey("IdCategoria")
                        .IsRequired();

                    b.HasOne("AppOscar.Models.Filme", "Filme")
                        .WithMany("Participantes")
                        .HasForeignKey("IdFilme")
                        .IsRequired();
                });

            modelBuilder.Entity("AppOscar.Models.Voto", b =>
                {
                    b.HasOne("AppOscar.Models.Participacao", "Participacao")
                        .WithMany("Votos")
                        .HasForeignKey("IdParticipacao")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
