using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto2PDS.DAOs;
using Proyecto2PDS.DTOs;
using Proyecto2PDS.Entities;


namespace Proyecto2PDS.Services
{
    public class ProfesoresService 
    {
        private readonly ProfesoresDAO _profesoresDAO;

        public ProfesoresService(ProfesoresDAO profesoresDAO)
        {
            _profesoresDAO = profesoresDAO;
        }

        public async Task<List<ProfesorDTO>> ObtenerTodosLosProfesoresAsync()
        {
            var profesores = await _profesoresDAO.ObtenerTodosAsync();            
            var profesoresDTO = profesores.Select(profesor => new ProfesorDTO
            {
                id = profesor.id,
                nombre = profesor.nombre,
                email = profesor.email,
                telefono = profesor.telefono,
                especializacion = profesor.especializacion
            }).ToList();            
            return profesoresDTO;
        }

        public async Task<ProfesorDTO> ObtenerProfesorPorIdAsync(int id)
        {
            var profesor = await _profesoresDAO.ObtenerPorIdAsync(id);
            if (profesor == null) return null;

            // Mapear Profesor a ProfesorDTO
            return new ProfesorDTO() {
                id = profesor.id,
                nombre = profesor.nombre,
                email = profesor.email,
                telefono = profesor.telefono,
                especializacion = profesor.especializacion
            };
        }

        public async Task CrearProfesorAsync(ProfesorDTO profesorDTO)
        {
            // Mapear ProfesorDTO a Profesor
            var profesor = new Profesor() {
                id = profesorDTO.id,
                nombre = profesorDTO.nombre,
                email = profesorDTO.email,
                telefono = profesorDTO.telefono,
                especializacion = profesorDTO.especializacion
            };

            await _profesoresDAO.AgregarAsync(profesor);

        }

        public async Task ActualizarProfesorAsync(ProfesorDTO profesorDTO)
        {
            // Mapear ProfesorDTO a Profesor
            var profesor = new Profesor() {
                id = profesorDTO.id,
                nombre = profesorDTO.nombre,
                email = profesorDTO.email,
                telefono = profesorDTO.telefono,
                especializacion = profesorDTO.especializacion
            };

            await _profesoresDAO.ActualizarAsync(profesor);
        }

        public async Task EliminarProfesorAsync(int id)
        {
            await _profesoresDAO.EliminarAsync(id);
        }
    }
}