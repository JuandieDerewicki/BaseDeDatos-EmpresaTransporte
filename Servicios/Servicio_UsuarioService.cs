using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class Servicio_UsuarioService : IServicioUsuarioService
    {
        private readonly MiDbContext _context;

        public Servicio_UsuarioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio_Usuario>> ObtenerTodosServiciosUsuariosAsync()
        {
            return await _context.Servicios_Usuarios.ToListAsync();
        }

        public async Task<Servicio_Usuario> ObtenerServicioUsuarioPorIdAsync(int id)
        {
            return await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);
        }

        public async Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorDniAsync(string dniUsuario)
        {
            return await _context.Servicios_Usuarios.Where(su => su.dni_usuario == dniUsuario).ToListAsync();
        }

        public async Task<Servicio_Usuario> AgregarServicioUsuarioAsync(Servicio_Usuario servicioUsuario)
        {
            _context.Servicios_Usuarios.Add(servicioUsuario);
            await _context.SaveChangesAsync();
            return servicioUsuario;
        }

        public async Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario)
        {
            var servicioUsuarioExistente = await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);

            if (servicioUsuarioExistente == null)
            {
                throw new Exception("Servicio de usuario no encontrado");
            }

            // Realizar las actualizaciones necesarias en servicioUsuarioExistente con los datos de servicioUsuario

            await _context.SaveChangesAsync();
            return servicioUsuarioExistente;
        }

        public async Task EliminarServicioUsuarioAsync(int id)
        {
            var servicioUsuarioExistente = await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);

            if (servicioUsuarioExistente == null)
            {
                throw new Exception("Servicio de usuario no encontrado");
            }

            _context.Servicios_Usuarios.Remove(servicioUsuarioExistente);
            await _context.SaveChangesAsync();
        }
    }
    public interface IServicioUsuarioService
    {
        Task<List<Servicio_Usuario>> ObtenerTodosServiciosUsuariosAsync();
        Task<Servicio_Usuario> ObtenerServicioUsuarioPorIdAsync(int id);
        Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorDniAsync(string dniUsuario);
        Task<Servicio_Usuario> AgregarServicioUsuarioAsync(Servicio_Usuario servicioUsuario);
        Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario);
        Task EliminarServicioUsuarioAsync(int id);
    }
}
