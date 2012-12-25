using System;
using Iesi.Collections.Generic;

namespace Centros.Model
{
    public class Taller : Entity
    {
        public string Nombre { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public Centro Centro { get; set; }

        public ISet<Participacion> Participaciones { get; private set; }
        // *Materiales entregados (descripcion, fecha)

        public override string ToString()
        {
            return Nombre;
        }
    }
}