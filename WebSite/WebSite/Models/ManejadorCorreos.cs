using Modelo.General;
using System.Collections.Generic;
using System.Configuration;
using Utiles.Notificaciones;

namespace WebSite.Models
{
	public class ManejadorCorreos
	{
		public ManejadorCorreos(string correoReceptor, string asunto)
		{
			string correoEmisor = ConfigurationManager.AppSettings["correoEmisor"];
			string smtpCliente = ConfigurationManager.AppSettings["smtpCliente"];
			string puertoCorreo = ConfigurationManager.AppSettings["puertoCorreo"];
			string nombreRemitente = ConfigurationManager.AppSettings["nombreRemitente"];
			string claveCorreo = ConfigurationManager.AppSettings["claveCorreo"];
			DatosCorreo = new DatosCorreo(correoEmisor, correoReceptor, smtpCliente, puertoCorreo, nombreRemitente, asunto, claveCorreo);
		}

        public void EstablecerCorreosConCopia(List<string> correosConCopia)
        {
            DatosCorreo.ListadoCorreosAEnviarConCopia = correosConCopia;
        }

		public void CrearCuerpoCorreo(string rutaPlantilla, Dictionary<string, string> diccionarioDatos)
		{
			DatosCorreo.CrearCuerpoCorreo(rutaPlantilla, diccionarioDatos);
		}

		public void EnviarCorreo()
		{
			Correo correo = new Correo();
			correo.EnviarCorreo(DatosCorreo);
		}

		public DatosCorreo DatosCorreo { set; get; }
	}
}