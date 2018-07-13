using Modelo.General;
using Modelo.Practicante;
using System;
using System.Collections.Generic;
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
	}
}