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
                .Where(ipi => ipi.PuntoIntermedio.nombre_ciudad == ciudadOrigen || ipi.PuntoIntermedio.nombre_ciudad == ciudadDestino)
                .ToListAsync();
        }

        public async Task<Itinerario_PuntoIntermedio> AgregarItinerarioPuntoIntermedioAsync(Itinerario_PuntoIntermedio itinerarioPuntoIntermedio)
        {
            _context.Itinerarios_PuntosIntermedios.Add(itinerarioPuntoIntermedio);
            await _context.SaveChangesAsync();
            return itinerarioPuntoIntermedio;
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
        Task<Itinerario_PuntoIntermedio> AgregarItinerarioPuntoIntermedioAsync(Itinerario_PuntoIntermedio itinerarioPuntoIntermedio);
        Task<Itinerario_PuntoIntermedio> EditarItinerarioPuntoIntermedioAsync(int id, Itinerario_PuntoIntermedio itinerarioPuntoIntermedio);
        Task EliminarItinerarioPuntoIntermedioAsync(int id);
    }
}

