using System;
using System.Collections.Generic;

namespace Proyecto2PDS.Entities;

public partial class Usuario
{
    public int ID { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Paterno { get; set; }

    public string? Materno { get; set; }

    public string? Nombre { get; set; }

    public string? Puesto { get; set; }

    public string? Password { get; set; }

    public ulong? Activo { get; set; }

    public virtual ICollection<Usuario_Accion> Usuario_Accion { get; set; } = new List<Usuario_Accion>();

    public virtual ICollection<usuario_sesion> usuario_sesion { get; set; } = new List<usuario_sesion>();
}
