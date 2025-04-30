using System;
using System.Collections.Generic;

namespace Proyecto2PDS.Entities;

public partial class curso_imagen
{
    public int ID { get; set; }

    public int? IDCurso { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual Curso? IDCursoNavigation { get; set; }
}
