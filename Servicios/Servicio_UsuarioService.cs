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

        //public async Task<Servicio_Usuario> AgregarServicioUsuarioAsync(Servicio_Usuario servicioUsuario)
        //{
        //    _context.Servicios_Usuarios.Add(servicioUsuario);
        //    await _context.SaveChangesAsync();
        //    return servicioUsuario;
        //}
        //public async Task<Servicio_Usuario> AgregarServicioUsuarioAsync(Servicio_Usuario servicioUsuario)
        //{
        //    // Calcular el costo final
        //    servicioUsuario.CalcularCostoFinal(servicioUsuario.Servicio);

        //    // Agregar el servicioUsuario al contexto
        //    _context.Servicios_Usuarios.Add(servicioUsuario);

        //    // Guardar los cambios en la base de datos
        //    await _context.SaveChangesAsync();

        //    return servicioUsuario;
        //}

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

        public async Task<Servicio_Usuario> AgregarServicioUsuarioYReservaAsync(Servicio_Usuario servicioUsuario)
        {
            // Calcular el costo final
            servicioUsuario.CalcularCostoFinal(servicioUsuario.Servicio);

            // Verificar la disponibilidad del servicio
            if (servicioUsuario.id_servicio.HasValue)
            {
                await VerificarDisponibilidadAsync(servicioUsuario.id_servicio.Value);
            }

            // Agregar el servicioUsuario al contexto
            _context.Servicios_Usuarios.Add(servicioUsuario);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return servicioUsuario;
        }

        private async Task VerificarDisponibilidadAsync(int idServicio)
        {
            var servicio = await _context.Servicios.FindAsync(idServicio);

            if (servicio == null)
            {
                // El servicio no existe
                throw new InvalidOperationException("El servicio no existe.");
            }

            if (servicio.disponibilidad <= 0)
            {
                // No hay disponibilidad de pasajes
                throw new InvalidOperationException("No hay disponibilidad de pasajes para este servicio.");
            }

            // Actualiza la disponibilidad en base a la cantidad de asientos de la unidad de transporte
            servicio.disponibilidad--;
            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }




        //public async Task<bool> VerificarDisponibilidadAsync(int idServicio)
        //{
        //    var servicio = await _context.Servicios.FindAsync(idServicio);

        //    if (servicio == null)
        //    {
        //        // El servicio no existe
        //        return false;
        //    }

        //    // Verifica la disponibilidad en base a la cantidad de asientos de la unidad de transporte
        //    return servicio.disponibilidad > 0;
        //}

        //public async Task<Servicio_Usuario> RealizarReservaAsync(Servicio_Usuario reserva)
        //{
        //    var servicio = await _context.Servicios.FindAsync(reserva.id_servicio);

        //    if (servicio == null)
        //    {
        //        // El servicio no existe
        //        throw new InvalidOperationException("El servicio no existe.");
        //    }

        //    if (servicio.disponibilidad <= 0)
        //    {
        //        // No hay disponibilidad de pasajes
        //        throw new InvalidOperationException("No hay disponibilidad de pasajes para este servicio.");
        //    }

        //    // Actualiza la disponibilidad en base a la cantidad de asientos de la unidad de transporte
        //    servicio.disponibilidad--;

        //    _context.Servicios.Update(servicio);
        //    await _context.SaveChangesAsync();

        //    // Agrega la reserva a la base de datos
        //    _context.Servicios_Usuarios.Add(reserva);
        //    await _context.SaveChangesAsync();

        //    return reserva;
        //}

        public async Task CancelarReservaAsync(int reservaId)
        {
            var reserva = await _context.Servicios_Usuarios.FindAsync(reservaId);

            if (reserva == null)
            {
                // La reserva no existe
                throw new InvalidOperationException("La reserva no existe.");
            }

            // Elimina la reserva de la base de datos
            _context.Servicios_Usuarios.Remove(reserva);
            await _context.SaveChangesAsync();

            // Incrementa la disponibilidad de pasajes
            var servicio = await _context.Servicios.FindAsync(reserva.id_servicio);
            if (servicio != null)
            {
                servicio.disponibilidad++;
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();
            }
        }

        //public async Task CancelarReservasAutomaticasAsync()
        //{
        //    // Lógica para cancelar automáticamente las reservas que han expirado
        //    // Este método debería ejecutarse automáticamente en segundo plano
        //    var reservasExpiradas = await _context.Servicios_Usuarios
        //        .Include(su => su.Servicio)  // Incluimos el Servicio y el Itinerario asociado
        //        .ThenInclude(s => s.Itinerario)
        //        .Where(r => r.Servicio.Itinerario.fechaHora_partida < DateTime.Now.AddMinutes(-30))
        //        .ToListAsync();

        //    foreach (var reserva in reservasExpiradas)
        //    {
        //        // Elimina la reserva de la base de datos
        //        _context.Servicios_Usuarios.Remove(reserva);

        //        // Incrementa la disponibilidad de pasajes
        //        var servicio = reserva.Servicio;
        //        if (servicio != null)
        //        {
        //            servicio.disponibilidad++;
        //            _context.Servicios.Update(servicio);
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}



    }

    public interface IServicioUsuarioService
    {
        Task<List<Servicio_Usuario>> ObtenerTodosServiciosUsuariosAsync();
        Task<Servicio_Usuario> ObtenerServicioUsuarioPorIdAsync(int id);
        Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorDniAsync(string dniUsuario);
        //Task<Servicio_Usuario> AgregarServicioUsuarioAsync(Servicio_Usuario servicioUsuario);
        Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario);
        Task EliminarServicioUsuarioAsync(int id);
        //Task<bool> VerificarDisponibilidadAsync(int idServicio);
        //Task<Servicio_Usuario> RealizarReservaAsync(Servicio_Usuario reserva);
        Task CancelarReservaAsync(int reservaId);
        //Task CancelarReservasAutomaticasAsync();
        Task<Servicio_Usuario> AgregarServicioUsuarioYReservaAsync(Servicio_Usuario servicioUsuario);

    }
}
