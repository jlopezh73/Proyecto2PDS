using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Proyecto2PDS.Services;
using Proyecto2PDS.DTOs;

namespace Proyecto2PDS.Pages.Cursos;

public class CursosModel : PageModel
{
    private ILogger<CursosModel> _logger;
    private readonly CursosService _cursosService;
    public CursoDTO Curso { get; set; }    
    public List<CursoDTO> Cursos { get; private set; }    

    [BindProperty]
    public int IdCursoEliminar { get; set; }
    

    public CursosModel(ILogger<CursosModel> logger, 
                       CursosService cursosService, 
                       ProfesoresService profesoresService, 
                       CursoDTO curso)
    {
        _cursosService = cursosService;        
        _logger = logger;
        Curso = curso;
    }

    public async Task OnGet()
    {
        Cursos = await _cursosService.ObtenerTodosLosCursosAsync();
        _logger.LogInformation($"Cursos obtenidos: {Cursos.Count}");   
    }

    public async Task OnPost()
    {
        await _cursosService.EliminarCursoAsync(IdCursoEliminar);
        Cursos = await _cursosService.ObtenerTodosLosCursosAsync();        
    }
}
    
