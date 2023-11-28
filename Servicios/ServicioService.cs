using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class ServicioService : IServicioService
    {
        private readonly MiDbContext _context;

        public ServicioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<Itinerario> ObtenerItinerarioAsync(int id)
        {
            return await _context.Itinerarios.FindAsync(id);
        }
        public async Task<List<Servicio>> ObtenerTodosServiciosAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio> ObtenerServicioPorIdAsync(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);
        }

        public async Task<List<Servicio>> ObtenerServiciosPorItinerarioAsync(int idItinerario)
        {
            return await _context.Servicios.Where(s => s.id_itinerario == idItinerario).ToListAsync();
        }

        //public async Task<Servicio> AgregarServicioAsync(Servicio servicio)
        //{
        //    _context.Servicios.Add(servicio);
        //    await _context.SaveChangesAsync();
        //    return servicio;
        //}

        public async Task<Servicio> AgregarServicioAsync(Servicio servicio)
        {
            //// ...

            //var itinerario = _context.Itinerarios.Find(servicio.id_itinerario);
            //if (itinerario == null)
            //{
            //    throw new Exception("El itinerario no existe");
            //}

            //servicio.Itinerario = itinerario;

            //// ...

            //return servicio;

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task<Servicio> EditarServicioAsync(int id, Servicio servicio)
        {
            var servicioExistente = await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);

            if (servicioExistente == null)
            {
                throw new Exception("Servicio no encontrado");
            }

            // Realizar las actualizaciones necesarias en el servicioExistente con los datos de servicio
            servicioExistente.costo_predeterminado = servicio.costo_predeterminado ?? servicioExistente.costo_predeterminado;
            servicioExistente.disponibilidad = servicio.disponibilidad;
            servicioExistente.id_itinerario = servicio.id_itinerario ?? servicioExistente.id_itinerario;
            servicioExistente.id_unidadTransporte = servicio.id_unidadTransporte ?? servicioExistente.id_unidadTransporte;

            await _context.SaveChangesAsync();
            return servicioExistente;
        }

        public async Task EliminarServicioAsync(int id)
        {
            var servicioExistente = await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);

            if (servicioExistente == null)
            {
                throw new Exception("Servicio no encontrado");
            }

            _context.Servicios.Remove(servicioExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IServicioService
    {
        Task<List<Servicio>> ObtenerTodosServiciosAsync();
        Task<Servicio> ObtenerServicioPorIdAsync(int id);
        Task<List<Servicio>> ObtenerServiciosPorItinerarioAsync(int idItinerario);
        Task<Servicio> AgregarServicioAsync(Servicio servicio);
        Task<Servicio> EditarServicioAsync(int id, Servicio servicio);
        Task EliminarServicioAsync(int id);
        Task<Itinerario> ObtenerItinerarioAsync(int id);
    }
}
