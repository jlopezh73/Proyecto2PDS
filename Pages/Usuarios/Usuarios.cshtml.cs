using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Proyecto2PDS.Services;
using Proyecto2PDS.DTOs;

namespace Proyecto2PDS.Pages.Cursos;

public class UsuariosModel : PageModel
{
    private ILogger<UsuariosModel> _logger;
    private readonly IdentidadService _identididadService;    
    public List<UsuarioDTO> Usuarios { get; private set; }    

    [BindProperty]
    public int IdUsuarioEliminar { get; set; }
    

    public UsuariosModel(ILogger<UsuariosModel> logger, 
                       IdentidadService identidadService)
    {
        _identididadService = identidadService;
        _logger = logger;        
    }

    public async Task OnGet()
    {
        Usuarios = await _identididadService.ObtenerTodosLosUsuariosAsync();        
    }

    public async Task OnPost()
    {
        await _identididadService.EliminarUsuarioAsync(IdUsuarioEliminar);
        Usuarios = await _identididadService.ObtenerTodosLosUsuariosAsync();        
    }
}
    
