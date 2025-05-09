using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto2PDS.DTOs
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public String CorreoElectronico {get; set;}
        public String nombreCompleto {get; set;}                
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public string? Nombre { get; set; }
        public string? Puesto { get; set; }
        public string? Password { get; set; }

    public ulong? Activo { get; set; }    
    }
}