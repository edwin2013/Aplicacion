using System;

namespace Modelo.Mantenimiento
{
	public class MultimediaInformacionModelo
	{
		public MultimediaInformacionModelo()
		{
			Datos = new byte[1];
		}

		public string Accion { set; get; }
		public Int64 MultimediaInformacionId { set; get; }
		public byte[] Datos { set; get; }
		public string Ruta { set; get; }
		public string Nombre { set; get; }
		public string ContentType { set; get; }
		public int InformacionId { set; get; }
		public int Tipo { set; get; }

		public string ConversionImagen
		{
			get
			{
				var base64 = Convert.ToBase64String(Datos);
				string conversionImagen = string.Format("data:" + ContentType + ";base64,{0}", base64);
				return conversionImagen;
			}
		}
	}
}
