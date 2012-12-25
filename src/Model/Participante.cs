using Iesi.Collections.Generic;

namespace Centros.Model
{
    public class Participante : Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Organizacion Organizacion { get; set; }
        public ISet<Taller> Participaciones { get; private set; }
    }
}