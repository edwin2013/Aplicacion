using Modelo.General;
using Modelo.Mantenimiento;
using Modelo.Paciente;
using Modelo.Practicante;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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

		[HttpGet]
		public ActionResult Calificacion(string identificadorGUID)
		{
			CitaModelo cita = new Negocios.NegociosPaciente().ObtenerCitas(identificadorGUID).FirstOrDefault();
			bool existeCita = cita != null;
			bool citaNoHaSidoCalificada = true;

			if (existeCita)
			{
				citaNoHaSidoCalificada = cita.Calificacion == 0;
				if (!citaNoHaSidoCalificada)
				{
					identificadorGUID = "YaFueCalificada";
				}
			}
			else
			{
				identificadorGUID = "NoExiste";
			}

			ViewBag.IdentificadorGUID = identificadorGUID;
			return View("Paciente");
		}

		public JsonResult EnviarCalificacion(CitaPracticanteModelo citaModelo)
		{
			CitaModelo cita =
			new Negocios.NegociosPaciente().ObtenerCitas(citaModelo.IdentificadorGUID).FirstOrDefault();

			citaModelo.CitaId = cita.CitaId;
			citaModelo.Accion = (char)Acciones.Actualizar;//ACCION DE ACTUALIZAR
			citaModelo.Antecedentes = cita.Antecedentes;
			citaModelo.Recomendaciones = cita.Recomendaciones;
			Mensaje mensajeRespuesta = new Negocios.NegociosPracticante().MantenimientoCita(citaModelo);
			if (mensajeRespuesta.Exito)
			{
				CitaPracticanteModelo citaPracticanteModelo = new Negocios.NegociosPracticante().ObtenerCitasPorId(cita.CitaId);
				string rutaServer = Server.MapPath("~/");
				string rutaPlantilla = rutaServer + ConfigurationManager.AppSettings["rutaPlantillaCitaCalificada"];
				string asunto = ConfigurationManager.AppSettings["asuntoCorreoCitaCalificada"];
				citaPracticanteModelo.Calificacion = citaModelo.Calificacion;
				Dictionary<string, string> datosCorreo = new DiccionarioDatos().CrearDiccionarioCitaCalificada(citaPracticanteModelo);

				ManejadorCorreos manejadorCorreos = new ManejadorCorreos("", asunto);
				manejadorCorreos.CrearCuerpoCorreo(rutaPlantilla, datosCorreo);
				int rolAdministrador = (int)Roles.Administrador;
				List<string> listaCorreosConCopia =
				new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolAdministrador).Select(item => item.Correo).ToList();
				manejadorCorreos.EstablecerMultiplesDestinatarios(listaCorreosConCopia);
				manejadorCorreos.EnviarCorreo();
			}

			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult CrearCita(CrearCitaModelo crearCitaModelo)
		{
			string identificadorGUID = new GeneracionCodigo().GenerarCodigoUnico();
			crearCitaModelo.CitaModelo.IdentificadorGUID = identificadorGUID;
			Mensaje mensajeRespuesta = new Negocios.NegociosPaciente().CrearCita(crearCitaModelo);

			if (mensajeRespuesta.Exito)
			{
				string rutaServer = Server.MapPath("~/");
				string rutaPlantilla = rutaServer + ConfigurationManager.AppSettings["rutaPlantilla"];
				string asunto = ConfigurationManager.AppSettings["asuntoCita"];
				Dictionary<string, string> datosPaciente = new DiccionarioDatos().CrearDiccionarioDatosPaciente(crearCitaModelo);

				ManejadorCorreos manejadorCorreos = new ManejadorCorreos(crearCitaModelo.PacienteModelo.CorreoElectronico, asunto);
				manejadorCorreos.CrearCuerpoCorreo(rutaPlantilla, datosPaciente);
				int rolAdministrador = (int)Roles.Administrador;
				List<string> listaCorreosConCopia =
				new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolAdministrador).Select(item => item.Correo).ToList();
				manejadorCorreos.EstablecerCorreosConCopia(listaCorreosConCopia);
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

		public ActionResult ObtenerTestimonios()
		{
			var vista = (string)null;
			string mensajeError = string.Empty;
			var datos = new { mensajeError = "", vistaHtml = "" };
			int tipoProximasActividades = (int)TipoInformacion.Testimonios;
			int consultaTodos = -1;
			List<InformacionModelo> lista = new List<InformacionModelo>(); ;
			try
			{
				lista = new Negocios.NegociosMantenimiento().ObtenerMultimediaInformacion(tipoProximasActividades, consultaTodos);
			}
			catch (Exception excepcion)
			{
				mensajeError = "Ocurrio un error consultando los datos.";
			}

			vista = ConvertirVistaString("/Views/Paciente/_Testimonios.cshtml", lista);
			datos = new { mensajeError = mensajeError, vistaHtml = vista };
			return Json(datos);
		}

		public ActionResult ObtenerMultimediaActividades(int informacionId)
		{
			var vista = (string)null;
			string mensajeError = string.Empty;
			var datos = new { mensajeError = "", vistaHtml = "" };

			List<MultimediaInformacionModelo> lista = new List<MultimediaInformacionModelo>(); ;
			InformacionModelo actividad = new InformacionModelo();
			try
			{
				lista = new Negocios.NegociosMantenimiento().ObtenerMultimediaInformacion(informacionId);
				actividad = new Negocios.NegociosMantenimiento().ObtenerInformacionPorId(informacionId).FirstOrDefault();
				ViewBag.Actividad = actividad;
			}
			catch (Exception excepcion)
			{
				mensajeError = "Ocurrio un error consultando los datos.";
			}

			vista = ConvertirVistaString("/Views/Paciente/_CarouselActividades.cshtml", lista);
			datos = new { mensajeError = mensajeError, vistaHtml = vista };
			return Json(datos);
		}

        public ActionResult ObtenerActividades()
        {
            var vista = (string)null;
            string mensajeError = string.Empty;
            var datos = new { mensajeError = "", vistaHtml = "" };
            int tipoProximasActividades = (int)TipoInformacion.ProximasActividades;
            int consultaTodos = -1;
            List<InformacionModelo> lista = new List<InformacionModelo>(); ;
            try
            {
                lista = new Negocios.NegociosMantenimiento().ObtenerMultimediaInformacion(tipoProximasActividades, consultaTodos);
            }
            catch (Exception excepcion)
            {
                mensajeError = "Ocurrio un error consultando los datos.";
            }

            vista = ConvertirVistaString("/Views/Paciente/_ProximasActividades.cshtml", lista);
            datos = new { mensajeError = mensajeError, vistaHtml = vista };
            return Json(datos);
        }

        private string ConvertirVistaString(string viewName, object model)
		{
			ViewData.Model = model;
			using (StringWriter writer = new StringWriter())
			{
				ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
				ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
				vResult.View.Render(vContext, writer);
				return writer.ToString();
			}
		}
	}
}