using System;

namespace Utiles
{
	public class GeneracionCodigo
	{
		public string GenerarCodigoUnico()
		{
			Guid guid = Guid.NewGuid();
			string codigoUnico = Convert.ToBase64String(guid.ToByteArray());
			codigoUnico = codigoUnico.Replace("=", "");
			codigoUnico = codigoUnico.Replace("+", "");

			return codigoUnico;
		}
	}
}
