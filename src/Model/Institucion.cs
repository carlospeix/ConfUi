namespace Centros.Model
{
	public class Institucion : Entity
	{
		public string Nombre { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string EMail { get; set; }
		public Jurisdiccion Jurisdiccion { get; set; }

		public override string ToString()
		{
			return string.Format("{0} ({1})", Nombre, Jurisdiccion.Nombre);
		}
	}
}