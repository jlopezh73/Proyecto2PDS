using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto2PDS.Services;
using Proyecto2PDS.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Proyecto2PDS.Pages.Cursos
{
    public class EditarCursoModel : PageModel
    {
        private readonly ILogger<EditarCursoModel> _logger;
        private  readonly CursosService _cursosService;
        private readonly ProfesoresService _profesoresService;
        
        [BindProperty]
        public CursoDTO Curso { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public List<ProfesorDTO> Profesores {get; set;}

        public Boolean CursoGuardadoExitoso { get; set; } = false;

        public EditarCursoModel(ILogger<EditarCursoModel> logger, 
                                CursosService cursosService, 
                                ProfesoresService profesoresService)
        {
            _logger = logger;
            _cursosService = cursosService;            
            _profesoresService = profesoresService;
        }        
        
        public async Task OnGet()
        {
            Profesores = await _profesoresService.ObtenerTodosLosProfesoresAsync();
            try
            {                                
                Curso = await _cursosService.ObtenerCursoPorIdAsync(Id);                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al recuperar el curso con ID {Id}");
                Curso = new CursoDTO();
            }
            Profesores = await _profesoresService.ObtenerTodosLosProfesoresAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cursosService.ActualizarCursoAsync(Curso);
                    CursoGuardadoExitoso = true;                    ;
                    //return RedirectToPage("Cursos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el curso");
            }

            Profesores = await _profesoresService.ObtenerTodosLosProfesoresAsync();
            return Page();
        }
    }
}