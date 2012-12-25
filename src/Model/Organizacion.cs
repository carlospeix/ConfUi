namespace Centros.Model
{
    public class Organizacion : Entity
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string EMail { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}