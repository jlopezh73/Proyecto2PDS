using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Proyecto2PDS.DTOs;

namespace Proyecto2PDS.Pages.Cursos;

public class CursoPartialModel 
{    
    public CursoDTO Curso { get; set; }    
    public List<ProfesorDTO> Profesores { get; set; }
        
}
    
