using System;

namespace Centros.Model
{
    public class Participacion : Entity
    {
        public Participante Participante { get; set; }
        public Taller Taller { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        // *Materiales entregados (descripcion, fecha)
    }
}