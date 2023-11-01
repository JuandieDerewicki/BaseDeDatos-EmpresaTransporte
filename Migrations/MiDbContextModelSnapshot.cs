﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViajePlusBDAPI;

#nullable disable

namespace ViajePlusBDAPI.Migrations
{
    [DbContext(typeof(MiDbContext))]
    partial class MiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Itinerario", b =>
                {
                    b.Property<int>("id_itinerario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_itinerario"));

                    b.Property<string>("ciudad_destino")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ciudad_origen")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("id_itinerario");

                    b.ToTable("Itinerarios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Itinerario_PuntoIntermedio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("id_itinerario")
                        .HasColumnType("int");

                    b.Property<int?>("id_puntoIntermedio")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("id_itinerario");

                    b.HasIndex("id_puntoIntermedio");

                    b.ToTable("Itinerario_PuntoIntermedios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.PuntoIntermedio", b =>
                {
                    b.Property<int>("id_puntoIntermedio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_puntoIntermedio"));

                    b.Property<string>("nombre_ciudad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_puntoIntermedio");

                    b.ToTable("PuntoIntermedios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Servicio", b =>
                {
                    b.Property<int>("id_servicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_servicio"));

                    b.Property<string>("calidad_servicio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("disponibilidad")
                        .HasColumnType("int");

                    b.Property<string>("fecha_llegada")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("fecha_partida")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("id_itinerario")
                        .HasColumnType("int");

                    b.Property<int?>("id_unidadTransporte")
                        .HasColumnType("int");

                    b.Property<string>("tipo_servicio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_servicio");

                    b.HasIndex("id_itinerario");

                    b.HasIndex("id_unidadTransporte");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Servicio_Usuario", b =>
                {
                    b.Property<int?>("id_servicio")
                        .HasColumnType("int");

                    b.Property<string>("dni_usuario")
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("costo")
                        .HasColumnType("float");

                    b.Property<string>("destino")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.HasKey("id_servicio", "dni_usuario");

                    b.HasIndex("dni_usuario");

                    b.ToTable("Servicio_Usuarios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.UnidadTransporte", b =>
                {
                    b.Property<int>("id_unidadTransporte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_unidadTransporte"));

                    b.Property<int>("asientos")
                        .HasColumnType("int");

                    b.Property<string>("categoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("tipo_unidad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_unidadTransporte");

                    b.ToTable("UnidadTransportes");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Usuario", b =>
                {
                    b.Property<string>("dni")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("contraseña")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("direccion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("fechaNacimiento")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("tipo_usuario")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("dni");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Itinerario_PuntoIntermedio", b =>
                {
                    b.HasOne("ViajePlusBDAPI.Modelos.Itinerario", "Itinerario")
                        .WithMany("Itinerario_PuntoIntermedios")
                        .HasForeignKey("id_itinerario");

                    b.HasOne("ViajePlusBDAPI.Modelos.PuntoIntermedio", "PuntoIntermedio")
                        .WithMany("Itinerario_PuntoIntermedios")
                        .HasForeignKey("id_puntoIntermedio");

                    b.Navigation("Itinerario");

                    b.Navigation("PuntoIntermedio");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Servicio", b =>
                {
                    b.HasOne("ViajePlusBDAPI.Modelos.Itinerario", "Itinerario")
                        .WithMany("Servicios")
                        .HasForeignKey("id_itinerario");

                    b.HasOne("ViajePlusBDAPI.Modelos.UnidadTransporte", "UnidadTransporte")
                        .WithMany("Servicios")
                        .HasForeignKey("id_unidadTransporte");

                    b.Navigation("Itinerario");

                    b.Navigation("UnidadTransporte");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Servicio_Usuario", b =>
                {
                    b.HasOne("ViajePlusBDAPI.Modelos.Usuario", "Usuario")
                        .WithMany("Servicio_Usuarios")
                        .HasForeignKey("dni_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViajePlusBDAPI.Modelos.Servicio", "Servicio")
                        .WithMany("Servicio_Usuarios")
                        .HasForeignKey("id_servicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Itinerario", b =>
                {
                    b.Navigation("Itinerario_PuntoIntermedios");

                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.PuntoIntermedio", b =>
                {
                    b.Navigation("Itinerario_PuntoIntermedios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Servicio", b =>
                {
                    b.Navigation("Servicio_Usuarios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.UnidadTransporte", b =>
                {
                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("ViajePlusBDAPI.Modelos.Usuario", b =>
                {
                    b.Navigation("Servicio_Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
