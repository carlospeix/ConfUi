namespace Centros.Model
{
    public class Jurisdiccion : Entity
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}