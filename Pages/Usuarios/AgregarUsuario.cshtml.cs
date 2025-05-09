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
    public class AgregarUsuarioModel : PageModel
    {
        private readonly ILogger<AgregarUsuarioModel> _logger;
        private  readonly IdentidadService _identidadService;        
        
        [BindProperty]
        public UsuarioDTO Usuario { get; set; }        
        

        public Boolean UsuarioGuardadoExitoso { get; set; } = false;

        public AgregarUsuarioModel(ILogger<AgregarUsuarioModel> logger, 
                          IdentidadService identidadService,
                          UsuarioDTO usuario)
        {
            _logger = logger;
            _identidadService = identidadService;
            Usuario = usuario;
        }        
        
        public async Task OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identidadService.CrearUsuarioAsync(Usuario);
                    UsuarioGuardadoExitoso = true;                    ;
                    //return RedirectToPage("Cursos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
            }
            
            return Page();
        }
    }
}