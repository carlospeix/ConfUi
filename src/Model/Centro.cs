namespace Centros.Model
{
    public class Centro : Entity
    {
        public Centro()
        {
            Horario = new Horario();
        }

        public string Nombre { get; set; }
        public Horario Horario { get; set; }
        public Institucion Institucion { get; set; }
        public Educador EducadorACargo { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}