using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto2PDS.Entities;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2PDS.DAOs
{
    public class CursosDAO
    {
        private readonly CursosContext _context;        

        public CursosDAO(CursosContext context)
        {
            _context = context;            
        }

        // Obtener todos los cursos
        public async Task<List<Curso>> ObtenerTodosAsync()
        {            
            return await _context.Cursos
                .Include(c => c.idProfesorNavigation) 
                .Include(c => c.CursoImagens) 
                .OrderBy(c => c.nombre)
                .ToListAsync();
        }

        // Obtener un curso por ID
        public async Task<Curso> ObtenerPorIdAsync(int id)
        {
            var curso =  await _context.Cursos                
                .Where( c=> c.id == id).FirstAsync();            
            return curso;
        }

        // Agregar un nuevo curso
        public async Task AgregarAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

        // Actualizar un curso existente
        public async Task ActualizarAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }

        // Eliminar un curso
        public async Task EliminarAsync(int id)
        {
            var curso = await ObtenerPorIdAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }
    }
}