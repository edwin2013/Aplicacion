namespace Modelo.General
{
	public class Mensaje
	{
		public Mensaje() { }
		public Mensaje(bool exito, string respuesta)
		{
			Exito = exito;
			Respuesta = respuesta;
		}

		public Mensaje(bool exito, string respuesta, int entidadId)
		{
			Exito = exito;
			Respuesta = respuesta;
			EntidadId = entidadId;
		}

		public bool Exito { set; get; }
		public string Respuesta { set; get; }
		public int EntidadId { set; get; }

	}
}
