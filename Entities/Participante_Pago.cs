using System;
using System.Collections.Generic;

namespace Proyecto2PDS.Entities;

public partial class Participante_Pago
{
    public int ID { get; set; }

    public int? IDParticipante { get; set; }

    public int? IDCurso { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Curso? IDCursoNavigation { get; set; }

    public virtual Participante? IDParticipanteNavigation { get; set; }
}
