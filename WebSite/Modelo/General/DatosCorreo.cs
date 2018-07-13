using System.Collections.Generic;
using System.IO;

namespace Modelo.General
{
	public class DatosCorreo
	{
		public DatosCorreo() { }
		public DatosCorreo(
			string correoEmisor,
			string correoReceptor,
			string smtpCliente,
			string puertoCorreo,
			string nombreRemitente,
			string asunto,
			string claveCorreo)
		{
			CorreoEmisor = correoEmisor;
			CorreoReceptor = correoReceptor;
			SmtpCliente = smtpCliente;
			PuertoCorreo = puertoCorreo;
			NombreRemitente = nombreRemitente;
			Asunto = asunto;
			ClaveCorreo = claveCorreo;

			EsCorreoMultiDestinatario = false;
			MultiplesRutasDeArchivosAdjuntos = new List<string>();
			MultiplesDestinatarios = new List<string>();
			CorreoAEnviarConCopia = string.Empty;
			ListadoCorreosAEnviarConCopia = new List<string>();

		}

		public void CrearCuerpoCorreo(string rutaPlantilla, Dictionary<string, string> diccionarioDatos)
		{
			DiccionarioDatos = diccionarioDatos;

			StreamReader reader = new StreamReader(rutaPlantilla);
			string readFile = reader.ReadToEnd();
			string cuerpoDelCorreo = "";
			cuerpoDelCorreo = readFile;

			foreach (KeyValuePair<string, string> datos in diccionarioDatos)
			{
				string nombreCampo = datos.Key;
				string valorCampo = datos.Value;

				cuerpoDelCorreo = cuerpoDelCorreo.Replace("$$" + nombreCampo + "$$", valorCampo);
			}

			CuerpoHTML = cuerpoDelCorreo.ToString();
		}

		public string CorreoEmisor { set; get; }
		public string CorreoReceptor { get; set; }
		public string SmtpCliente { set; get; }
		public string NombreRemitente { set; get; }
		public string Asunto { set; get; }
		public string CuerpoHTML { set; get; }
		public bool EsCorreoMultiDestinatario { set; get; }
		public string CorreoAEnviarConCopia { get; set; }
		public string CorreoAEnviarConCopiaOculta { get; set; }
		public string PuertoCorreo { get; set; }
		public string ClaveCorreo { get; set; }
		public List<string> MultiplesRutasDeArchivosAdjuntos { get; set; }
		public List<string> MultiplesDestinatarios { get; set; }
		public List<string> ListadoCorreosAEnviarConCopia { get; set; }

		public Dictionary<string, string> DiccionarioDatos { get; set; }
	}
}