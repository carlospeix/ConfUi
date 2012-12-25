namespace Centros.Model
{
    public class Educador : Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string EMail { get; set; }

        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }

        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}