using Modelo.General;
using Modelo.Paciente;
using Modelo.Practicante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebSite.Controllers
{
	public class PracticanteController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Oferta()
		{
			return View();
		}

		public JsonResult MantenimientoOfertaHorario(OfertaHorarioModelo ofertaHorarioModelo)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosPracticante().MantenimientoOfertaHorario(ofertaHorarioModelo);
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public ActionResult CitasPracticante()
		{
			return View();
		}

		public JsonResult ObtenerCitasPracticante(FiltroCitas filtroCitas)
		{

			List<CitaPracticanteModelo> listaCitasPracticante = new Negocios.NegociosPracticante().ObtenerCitasPracticante(filtroCitas);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaCitasPracticante);

			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult MantenimientoCita(CitaPracticanteModelo citaModelo)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosPracticante().MantenimientoCita(citaModelo);
			bool citaFueCerrada = citaModelo.Accion == "A" && mensajeRespuesta.Exito;
			if (citaFueCerrada)
			{
				//Enviar correo
			}
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerOfertaPracticante(FiltroCitas filtroCitas)
		{
			filtroCitas.UsuarioId = 1;//TODO

			List<OfertaPracticante> listaOfertaPracticante = new Negocios.NegociosPracticante().ObtenerOfertaPracticante(filtroCitas);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaOfertaPracticante);

			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ActualizarPaciente(PacienteModelo pacienteModelo)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosPaciente().ActualizarPaciente(pacienteModelo);
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerPacientes(int pacienteId)
		{
			PacienteModelo pacienteModelo = new Negocios.NegociosPaciente().ObtenerPacientes(pacienteId).FirstOrDefault();
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(pacienteModelo);

			return Json(datos, JsonRequestBehavior.AllowGet);
		}
	}
}