using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Proyecto2PDS.DTOs;
using Proyecto2PDS.Services;
using Proyecto2PDS.Services;
namespace Proyecto2PDS.Pages.Profesores;

public class ProfesoresModel : PageModel
{
    private ILogger<ProfesoresModel> _logger;
    private readonly ProfesoresService _profesoresService;

    public List<ProfesorDTO> Profesores { get; set; }
    public ProfesorDTO _Profesor { get; set; }

    public ProfesoresModel(ILogger<ProfesoresModel> logger, 
                           ProfesoresService profesoresService,
                           ProfesorDTO Profesor)
    {
        _profesoresService = profesoresService;
        _Profesor = Profesor;
        _logger = logger;
    }

    public async Task OnGet()
    {
        Profesores = await _profesoresService.ObtenerTodosLosProfesoresAsync();
        if (Profesores == null)
        {
            Profesores = new List<ProfesorDTO>();
        }        
    }
}
    
