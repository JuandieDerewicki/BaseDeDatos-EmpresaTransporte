using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ViajePlusBDAPI
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<PuntoIntermedio> PuntosIntermedios { get; set; }
        public DbSet<Itinerario_PuntoIntermedio> Itinerario_PuntoIntermedios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Servicio_Usuario> Servicio_Usuarios { get; set; }
        public DbSet<UnidadTransporte> UnidadesTransporte { get; set; }
        public DbSet<Rol> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir la relación 1:N entre Itinerario y PuntoIntermedio a través de Itinerario_PuntoIntermedio
            modelBuilder.Entity<Itinerario_PuntoIntermedio>()
                .HasOne(ip => ip.Itinerario)
                .WithMany(i => i.Itinerario_PuntoIntermedios)
                .HasForeignKey(ip => ip.id_itinerario)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Itinerario_PuntoIntermedio>()
                .HasOne(ip => ip.PuntoIntermedio)
                .WithMany(p => p.Itinerario_PuntoIntermedios)
                .HasForeignKey(ip => ip.id_puntoIntermedio)
                .OnDelete(DeleteBehavior.Restrict);

            // Definir la relación 1:N entre Itinerario y Servicio
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Itinerario)
                .WithMany(i => i.Servicios)
                .HasForeignKey(s => s.id_itinerario)
                .OnDelete(DeleteBehavior.Cascade);

            // Definir la relación N:1 entre UnidadTransporte y Servicio
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.UnidadTransporte)
                .WithMany(u => u.Servicios)
                .HasForeignKey(s => s.id_unidadTransporte)
                .OnDelete(DeleteBehavior.Cascade);

            // Definir la relación M:N entre Servicio y Usuario a través de Servicio_Usuario
            modelBuilder.Entity<Servicio_Usuario>()
                .HasKey(su => new { su.id_servicio, su.dni_usuario });

            modelBuilder.Entity<Servicio_Usuario>()
                .HasOne(su => su.Servicio)
                .WithMany(s => s.Servicio_Usuarios)
                .HasForeignKey(su => su.id_servicio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Servicio_Usuario>()
                .HasOne(su => su.Usuario)
                .WithMany(u => u.Servicio_Usuarios)
                .HasForeignKey(su => su.dni_usuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicio_Usuario>()
                  .HasOne(su => su.PuntoIntermedio)
                  .WithOne()
                  .HasForeignKey<Servicio_Usuario>(su => su.id_puntoIntermedio)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuarios-Roles (1:N)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.RolesUsuarios)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.id_rol);

            /*
           La eliminación por cascada se aplica a:

           Entre Itinerario y Servicio: Si eliminamos un itinerario, todos los servicios asociados a ese itinerario se eliminan también
           Entre Servicio y UnidadTransporte: Si eliminamos una unidad de transporte, todos los servicios asociados a esa unidad de transporte también deberían eliminarse también.
           Entre Servicio_Usuario y Servicio: Si eliminamos un servicio, todas las reservas de ese servicio también deberían eliminarse también.

           La eliminación por cascada no debería aplicarse:
           Entre Itinerario y PuntoIntermedio: Si eliminamos un punto intermedio, no debería eliminarse el itinerario al que pertenece.
           Entre Servicio_Usuario y Usuario: Si eliminamos un usuario, no deberían eliminarse las reservas de ese usuario.

            */


        }

    }
}
