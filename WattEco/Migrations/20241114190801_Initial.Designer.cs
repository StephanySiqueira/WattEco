﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using WattEco.Persistence;

#nullable disable

namespace WattEco.Migrations
{
    [DbContext(typeof(OracleDbContext))]
    [Migration("20241114190801_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WattEco.Models.Historico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_HISTORICO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataHistorico")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATA_HISTORICO");

                    b.Property<int>("MissaoId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_MISSAO");

                    b.Property<int?>("Pontos")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTOS");

                    b.Property<int>("RecompensaId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RECOMPENSA");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("MissaoId");

                    b.HasIndex("RecompensaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("HISTORICO_WATT");
                });

            modelBuilder.Entity("WattEco.Models.Missao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_MISSAO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("DESCRICAO");

                    b.Property<int>("Pontuacao")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTUACAO");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("MISSAO_WATT");
                });

            modelBuilder.Entity("WattEco.Models.Recompensa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RECOMPENSA");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("DESCRICAO_RECOMPENSA");

                    b.Property<int>("PontosNecessarios")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PONTOS_NECESSARIOS");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RECOMPENSA_WAITT");
                });

            modelBuilder.Entity("WattEco.Models.Relatorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_RELATORIO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("ConsumoKwh")
                        .HasColumnType("BINARY_FLOAT")
                        .HasColumnName("CONSUMO_KWH");

                    b.Property<float>("EmissaoCO2")
                        .HasColumnType("BINARY_FLOAT")
                        .HasColumnName("EMISSAO_CO2");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PERIODO");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RELATORIO_WATT");
                });

            modelBuilder.Entity("WattEco.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID_USUARIO");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("EMAIL_USUARIO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NOME_USUARIO");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("SENHA_USUARIO");

                    b.HasKey("Id");

                    b.ToTable("USUARIO_WATT");
                });

            modelBuilder.Entity("WattEco.Models.Historico", b =>
                {
                    b.HasOne("WattEco.Models.Missao", "Missao")
                        .WithMany()
                        .HasForeignKey("MissaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WattEco.Models.Recompensa", "Recompensa")
                        .WithMany()
                        .HasForeignKey("RecompensaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WattEco.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Missao");

                    b.Navigation("Recompensa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WattEco.Models.Missao", b =>
                {
                    b.HasOne("WattEco.Models.Usuario", "Usuario")
                        .WithMany("MissoesConcluidas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WattEco.Models.Recompensa", b =>
                {
                    b.HasOne("WattEco.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WattEco.Models.Relatorio", b =>
                {
                    b.HasOne("WattEco.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WattEco.Models.Usuario", b =>
                {
                    b.Navigation("MissoesConcluidas");
                });
#pragma warning restore 612, 618
        }
    }
}
