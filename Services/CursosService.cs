using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto2PDS.DTOs;
using Proyecto2PDS.DAOs;
using Proyecto2PDS.Entities;

namespace Proyecto2PDS.Services
{
    public class CursosService 
    {
        private readonly CursosDAO _dao;

        public CursosService(CursosDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<CursoDTO>> ObtenerTodosLosCursosAsync()
        {
            var cursosAsync =  await _dao.ObtenerTodosAsync();
            var dtos = cursosAsync.Select(c => new CursoDTO
            {
                id = c.id,
                nombre = c.nombre,
                descripcion = c.descripcion,
                precio = c.precio,
                fechaInicio = c.fechaInicio,
                fechaTermino = c.fechaTermino,
                idProfesor = c.idProfesor,
                profesor = c.idProfesorNavigation?.nombre??""
            }).ToList();
            return dtos;
        }

        public async Task<CursoDTO> ObtenerCursoPorIdAsync(int id)
        {
            var c = await _dao.ObtenerPorIdAsync(id);
            return  new CursoDTO
            {
                id = c.id,
                nombre = c.nombre,
                descripcion = c.descripcion,
                precio = c.precio,
                fechaInicio = c.fechaInicio,
                fechaTermino = c.fechaTermino,
                idProfesor = c.idProfesor                
            };
        }

        public async Task CrearCursoAsync(CursoDTO c)
        {
            var curso =  new Curso
            {
                id = c.id,
                nombre = c.nombre,
                descripcion = c.descripcion,
                precio = c.precio,
                fechaInicio = c.fechaInicio,
                fechaTermino = c.fechaTermino,
                idProfesor = c.idProfesor
            };
            await _dao.AgregarAsync(curso);
        }

        public async Task ActualizarCursoAsync(CursoDTO c)
        {
            var curso =  new Curso
            {
                id = c.id,
                nombre = c.nombre,
                descripcion = c.descripcion,
                precio = c.precio,
                fechaInicio = c.fechaInicio,
                fechaTermino = c.fechaTermino,
                idProfesor = c.idProfesor
            };
            await _dao.ActualizarAsync(curso);
        }

        public async Task EliminarCursoAsync(int id)
        {
            await _dao.EliminarAsync(id);
        }
    }
}