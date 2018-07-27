namespace Modelo.Mantenimiento
{
	public class InformacionModelo
	{
		public InformacionModelo()
		{
			Multimedia = new MultimediaInformacionModelo();
		}

		public string Accion { set; get; }
		public int InformacionId { set; get; }
		public string Fecha { set; get; }
		public int Cupo { set; get; }
		public string Descripcion { set; get; }
		public string Titulo { set; get; }
		public bool Activo { set; get; }
		public int Tipo { set; get; }
		public MultimediaInformacionModelo Multimedia { set; get; }
	}
}
