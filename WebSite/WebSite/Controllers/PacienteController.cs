using Modelo.General;
using Modelo.Paciente;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;
using WebSite.Models;

namespace WebSite.Controllers
{
	public class PacienteController : Controller
	{
		// GET: Paciente
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Paciente()
		{
			return View();
		}

		public JsonResult CrearCita(CrearCitaModelo crearCitaModelo)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosPaciente().CrearCita(crearCitaModelo);

			if (mensajeRespuesta.Exito)
			{
				string asunto = ConfigurationManager.AppSettings["asuntoCita"];
				ManejadorCorreos manejadorCorreos = new ManejadorCorreos(crearCitaModelo.PacienteModelo.CorreoElectronico, asunto);
				Dictionary<string, string> datosPaciente = new DiccionarioDatos().CrearDiccionarioDatosPaciente(crearCitaModelo);
				string rutaPlantilla = ConfigurationManager.AppSettings["rutaPlantilla"];
				manejadorCorreos.CrearCuerpoCorreo(rutaPlantilla, datosPaciente);
				manejadorCorreos.EnviarCorreo();
			}

			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerDiasOfertaMes(string fechaOferta)
		{
			string[] fecha = fechaOferta.Split('/');
			int mes = Convert.ToInt32(fecha[0]);
			int anio = Convert.ToInt32(fecha[1]);

			List<DiasOfertaMes> listaDiasOfertaMes = new Negocios.NegociosPaciente().ObtenerDiasOfertaMes(mes, anio);
			var datos = new JavaScriptSerializer().Serialize(listaDiasOfertaMes);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerSesionesActivas(string fechaDia)
		{
			List<SesionModelo> listaSesionesDisponibles = new Negocios.NegociosPaciente().ObtenerSesionesActivas(fechaDia);
			var datos = new JavaScriptSerializer().Serialize(listaSesionesDisponibles);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}
	}
}