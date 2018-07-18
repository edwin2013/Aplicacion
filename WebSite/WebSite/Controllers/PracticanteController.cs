﻿using Modelo.General;
using Modelo.Paciente;
using Modelo.Practicante;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;
using WebSite.Models;
using WebSite.Models.Seguridad;

namespace WebSite.Controllers
{
    public class PracticanteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		[FiltroPermiso(Permiso = RolesPermisos.Alumno_Puede_Visualizar_Un_Alumno)]
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
                string asunto = ConfigurationManager.AppSettings["asuntoCorreoCalificacion"];
                ManejadorCorreos manejadorCorreos = new Models.ManejadorCorreos(citaModelo.CorreoElectronico, asunto);
                string rutaPagina =
                System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Paciente/Calificacion?identificadorGUID=" + citaModelo.IdentificadorGUID;
                Dictionary<string, string> datosPaciente = new DiccionarioDatos().CrearDiccionarioCorreoCalificacion(citaModelo.Paciente, rutaPagina);
                string rutaPlantilla = ConfigurationManager.AppSettings["rutaPlantillaCalificacion"];
                manejadorCorreos.CrearCuerpoCorreo(rutaPlantilla, datosPaciente);
                manejadorCorreos.EnviarCorreo();
            }
            var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerOfertaPracticante(FiltroCitas filtroCitas)
        {

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