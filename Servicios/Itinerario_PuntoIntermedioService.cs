using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class Itinerario_PuntoIntermedioService : IItinerarioPuntoIntermedioService
    {
        private readonly MiDbContext _context;

        public Itinerario_PuntoIntermedioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Itinerario_PuntoIntermedio>> ObtenerTodosItinerariosPuntoIntermedioAsync()
        {
            return await _context.Itinerarios_PuntosIntermedios.ToListAsync();
        }

        public async Task<Itinerario_PuntoIntermedio> ObtenerItinerarioPuntoIntermedioPorIdAsync(int id)
        {
            return await _context.Itinerarios_PuntosIntermedios.FindAsync(id);
        }

        public async Task<List<Itinerario_PuntoIntermedio>> ObtenerItinerariosPuntoIntermedioPorCiudadAsync(string ciudad)
        {
            return await _context.Itinerarios_PuntosIntermedios
                .Where(ipi => ipi.PuntoIntermedio.nombre_ciudad == ciudad)
                .ToListAsync();
        }

        public async Task<List<Itinerario_PuntoIntermedio>> ObtenerItinerariosPuntoIntermedioPorCiudadOrigenYDestinoAsync(string ciudadOrigen, string ciudadDestino)
        {
            return await _context.Itinerarios_PuntosIntermedios
                .Where(ipi => ipi.Itinerario.ciudad_origen == ciudadOrigen || ipi.Itinerario.ciudad_destino == ciudadDestino)
                .ToListAsync();
        }

        public async Task<List<Itinerario_PuntoIntermedio>> ObtenerPuntosIntermediosPorItinerarioAsync(int idItinerario)
        {
            return await _context.Itinerarios_PuntosIntermedios
                .Where(ipi => ipi.id_itinerario == idItinerario)
                .ToListAsync();
        }

        public async Task<Itinerario_PuntoIntermedio> AgregarItinerarioPuntoIntermedioAsync(Itinerario_PuntoIntermedio itinerarioPuntoIntermedio)
        {
            // Verificar si existen los Itinerario y PuntoIntermedio correspondientes
            var itinerarioExistente = await _context.Itinerarios.FindAsync(itinerarioPuntoIntermedio.id_itinerario);
            var puntoIntermedioExistente = await _context.PuntosIntermedios.FindAsync(itinerarioPuntoIntermedio.id_puntoIntermedio);

            if (itinerarioExistente == null || puntoIntermedioExistente == null)
            {
                // Uno o ambos no existen, manejar el error según sea necesario
                throw new Exception("El Itinerario o PuntoIntermedio no existe.");
            }

            // Asignar las referencias de navegación
            itinerarioPuntoIntermedio.Itinerario = itinerarioExistente;
            itinerarioPuntoIntermedio.PuntoIntermedio = puntoIntermedioExistente;

            // Agregar y guardar cambios
            _context.Itinerarios_PuntosIntermedios.Add(itinerarioPuntoIntermedio);
            await _context.SaveChangesAsync();

            return itinerarioPuntoIntermedio;
            //_context.Itinerarios_PuntosIntermedios.Add(itinerarioPuntoIntermedio);
            //await _context.SaveChangesAsync();
            //return itinerarioPuntoIntermedio;
        }

        public async Task<Itinerario_PuntoIntermedio> EditarItinerarioPuntoIntermedioAsync(int id, Itinerario_PuntoIntermedio itinerarioPuntoIntermedio)
        {
            var itinerarioPuntoIntermedioExistente = await _context.Itinerarios_PuntosIntermedios.FindAsync(id);

            if (itinerarioPuntoIntermedioExistente == null)
            {
                throw new Exception("Itinerario_PuntoIntermedio no encontrado");
            }

            itinerarioPuntoIntermedioExistente.id_itinerario = itinerarioPuntoIntermedio.id_itinerario;
            itinerarioPuntoIntermedioExistente.id_puntoIntermedio = itinerarioPuntoIntermedio.id_puntoIntermedio;
            itinerarioPuntoIntermedioExistente.hora_llegada_PI = itinerarioPuntoIntermedio.hora_llegada_PI;
            itinerarioPuntoIntermedioExistente.hora_salida_PI = itinerarioPuntoIntermedio.hora_salida_PI;

            await _context.SaveChangesAsync();

            return itinerarioPuntoIntermedioExistente;
        }

        public async Task EliminarItinerarioPuntoIntermedioAsync(int id)
        {
            var itinerarioPuntoIntermedioExistente = await _context.Itinerarios_PuntosIntermedios.FindAsync(id);

            if (itinerarioPuntoIntermedioExistente == null)
            {
                throw new Exception("Itinerario_PuntoIntermedio no encontrado");
            }

            _context.Itinerarios_PuntosIntermedios.Remove(itinerarioPuntoIntermedioExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IItinerarioPuntoIntermedioService
    {
        Task<List<Itinerario_PuntoIntermedio>> ObtenerTodosItinerariosPuntoIntermedioAsync();
        Task<Itinerario_PuntoIntermedio> ObtenerItinerarioPuntoIntermedioPorIdAsync(int id);
        Task<List<Itinerario_PuntoIntermedio>> ObtenerItinerariosPuntoIntermedioPorCiudadAsync(string ciudad);
        Task<List<Itinerario_PuntoIntermedio>> ObtenerItinerariosPuntoIntermedioPorCiudadOrigenYDestinoAsync(string ciudadOrigen, string ciudadDestino);
        Task<List<Itinerario_PuntoIntermedio>> ObtenerPuntosIntermediosPorItinerarioAsync(int idItinerario); 
        Task<Itinerario_PuntoIntermedio> AgregarItinerarioPuntoIntermedioAsync(Itinerario_PuntoIntermedio itinerarioPuntoIntermedio);
        Task<Itinerario_PuntoIntermedio> EditarItinerarioPuntoIntermedioAsync(int id, Itinerario_PuntoIntermedio itinerarioPuntoIntermedio);
        Task EliminarItinerarioPuntoIntermedioAsync(int id);
    }
}

