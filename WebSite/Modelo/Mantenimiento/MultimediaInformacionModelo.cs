using System;

namespace Modelo.Mantenimiento
{
	public class MultimediaInformacionModelo
	{
		public string Accion { set; get; }
		public Int64 MultimediaInformacionId { set; get; }
		public byte[] Datos { set; get; }
		public string Ruta { set; get; }
		public int InformacionId { set; get; }
		public int Tipo { set; get; }
	}
}
