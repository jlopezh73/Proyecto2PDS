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
    public class AgregarCursoModel : PageModel
    {
        private readonly ILogger<AgregarCursoModel> _logger;
        private  readonly CursosService _cursosService;
        private readonly ProfesoresService _profesoresService;
        
        [BindProperty]
        public CursoDTO Curso { get; set; }
        

        [BindProperty]
        public List<ProfesorDTO> Profesores {get; set;}

        public Boolean CursoGuardadoExitoso { get; set; } = false;

        public AgregarCursoModel(ILogger<AgregarCursoModel> logger, 
                          CursosService cursosService, 
                          ProfesoresService profesoresService,
                          CursoDTO curso)
        {
            _logger = logger;
            _cursosService = cursosService;            
            _profesoresService = profesoresService;
            Curso = curso;
        }        
        
        public async Task OnGet()
        {
            Profesores = await _profesoresService.ObtenerTodosLosProfesoresAsync();            
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cursosService.CrearCursoAsync(Curso);
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