﻿using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class RolService : IRolService
    {
        // métodos para interactuar con la entidad de Roles en la BD

        private readonly MiDbContext _context; // Representa el contexto de la base de datos. El contexto se utiliza para interactuar con la BD y y realizar operaciones de lectura en Roles

        public RolService(MiDbContext context)
        {
            _context = context;
        }

        //Lista de los roles almacenados en la BD
        public async Task<List<Rol>> ObtenerTodosRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        // Recibe un id y busca un rol con ese id en la BD
        public async Task<Rol> ObtenerRolPorIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.id_rol == id);
        }


        public async Task<Rol> CrearRolAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> ObtenerRolPorTipoAsync(string tipoRol)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.tipo_rol == tipoRol);
        }
    }

    // La interfaz IRolesService define los métodos que debe implementar RolesService y proporciona una abstracción para interactuar con la entidad de Roles.
    public interface IRolService
    {
        Task<List<Rol>> ObtenerTodosRolesAsync();
        Task<Rol> ObtenerRolPorIdAsync(int id);
        Task<Rol> CrearRolAsync(Rol rol);
        Task<Rol> ObtenerRolPorTipoAsync(string tipoRol);
    }
}

