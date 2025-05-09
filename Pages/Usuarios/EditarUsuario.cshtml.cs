using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto2PDS.Services;
using Proyecto2PDS.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Proyecto2PDS.Pages.Usuarios
{
    public class EditarUsuarioModel : PageModel
    {
        private readonly ILogger<EditarUsuarioModel> _logger;
        private  readonly IdentidadService _identidadService;        
        
        [BindProperty]
        public UsuarioDTO Usuario { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        
        public Boolean CursoGuardadoExitoso { get; set; } = false;

        public EditarUsuarioModel(ILogger<EditarUsuarioModel> logger, 
                                IdentidadService identidadService)
        {
            _logger = logger;
            _identidadService = identidadService;
        }        
        
        public async Task OnGet()
        {            
            try
            {                                
                Usuario = await _identidadService.ObtenerUsuarioPorIdAsync(Id);                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al recuperar el curso con ID {Id}");
                Usuario = new UsuarioDTO();
            }            
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identidadService.ActualizarUsuarioAsync(Usuario);
                    CursoGuardadoExitoso = true;                    ;
                    //return RedirectToPage("Cursos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el curso");
            }
            
            return Page();
        }
    }
}