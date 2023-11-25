using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class PuntoIntermedioService : IPuntoIntermedioService
    {
        private readonly MiDbContext _context;

        public PuntoIntermedioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<PuntoIntermedio>> ObtenerTodosPuntosIntermediosAsync()
        {
            return await _context.PuntosIntermedios.ToListAsync();
        }

        public async Task<PuntoIntermedio> ObtenerPuntoIntermedioPorIdAsync(int id)
        {
            return await _context.PuntosIntermedios.FindAsync(id);
        }

        public async Task<PuntoIntermedio> ObtenerPuntoIntermedioPorNombreAsync(string nombre)
        {
            return await _context.PuntosIntermedios.FirstOrDefaultAsync(p => p.nombre_ciudad == nombre);
        }

        public async Task<PuntoIntermedio> AgregarPuntoIntermedioAsync(PuntoIntermedio puntoIntermedio)
        {
            _context.PuntosIntermedios.Add(puntoIntermedio);
            await _context.SaveChangesAsync();
            return puntoIntermedio;
        }

        public async Task<PuntoIntermedio> EditarPuntoIntermedioAsync(int id, PuntoIntermedio puntoIntermedio)
        {
            var puntoExistente = await _context.PuntosIntermedios.FindAsync(id);

            if (puntoExistente == null)
            {
                throw new Exception("Punto intermedio no encontrado");
            }

            puntoExistente.nombre_ciudad = puntoIntermedio.nombre_ciudad;

            await _context.SaveChangesAsync();

            return puntoExistente;
        }

        public async Task EliminarPuntoIntermedioAsync(int id)
        {
            var puntoExistente = await _context.PuntosIntermedios.FindAsync(id);

            if (puntoExistente == null)
            {
                throw new Exception("Punto intermedio no encontrado");
            }

            _context.PuntosIntermedios.Remove(puntoExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IPuntoIntermedioService
    {
        Task<List<PuntoIntermedio>> ObtenerTodosPuntosIntermediosAsync();
        Task<PuntoIntermedio> ObtenerPuntoIntermedioPorIdAsync(int id);
        Task<PuntoIntermedio> ObtenerPuntoIntermedioPorNombreAsync(string nombre);
        Task<PuntoIntermedio> AgregarPuntoIntermedioAsync(PuntoIntermedio puntoIntermedio);
        Task<PuntoIntermedio> EditarPuntoIntermedioAsync(int id, PuntoIntermedio puntoIntermedio);
        Task EliminarPuntoIntermedioAsync(int id);
    }
}

