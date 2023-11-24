using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<PuntoIntermedio> PuntoIntermedios { get; set; }
        public DbSet<Itinerario_PuntoIntermedio> Itinerario_PuntoIntermedios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Servicio_Usuario> Servicio_Usuarios { get; set; }
        public DbSet<UnidadTransporte> UnidadTransportes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir la relación 1:N entre Itinerario y PuntoIntermedio a través de Itinerario_PuntoIntermedio
            modelBuilder.Entity<Itinerario_PuntoIntermedio>()
                .HasOne(ip => ip.Itinerario)
                .WithMany(i => i.Itinerario_PuntoIntermedios)
                .HasForeignKey(ip => ip.id_itinerario);

            modelBuilder.Entity<Itinerario_PuntoIntermedio>()
                .HasOne(ip => ip.PuntoIntermedio)
                .WithMany(p => p.Itinerario_PuntoIntermedios)
                .HasForeignKey(ip => ip.id_puntoIntermedio);

            // Definir la relación 1:N entre Itinerario y Servicio
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Itinerario)
                .WithMany(i => i.Servicios)
                .HasForeignKey(s => s.id_itinerario);

            // Definir la relación N:1 entre UnidadTransporte y Servicio
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.UnidadTransporte)
                .WithMany(u => u.Servicios)
                .HasForeignKey(s => s.id_unidadTransporte);

            // Definir la relación M:N entre Servicio y Usuario a través de Servicio_Usuario
            modelBuilder.Entity<Servicio_Usuario>()
                .HasKey(su => new { su.id_servicio, su.dni_usuario });

            modelBuilder.Entity<Servicio_Usuario>()
                .HasOne(su => su.Servicio)
                .WithMany(s => s.Servicio_Usuarios)
                .HasForeignKey(su => su.id_servicio);

            modelBuilder.Entity<Servicio_Usuario>()
                .HasOne(su => su.Usuario)
                .WithMany(u => u.Servicio_Usuarios)
                .HasForeignKey(su => su.dni_usuario);

            modelBuilder.Entity<Servicio_Usuario>()
                  .HasOne(su => su.PuntoIntermedio)
                  .WithOne()
                  .HasForeignKey<Servicio_Usuario>(su => su.id_puntoIntermedio);
                  //.OnDelete(DeleteBehavior.Restrict);
                  //.OnDelete(DeleteBehavior.Cascade);


        }

    }
}
