using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto2PDS.DTOs;

public class RespuestaValidacionUsuarioDTO
{
    public bool correcto {get; set;}
    public UsuarioDTO? usuario {get; set;}
    public string token {get; set;}
}
