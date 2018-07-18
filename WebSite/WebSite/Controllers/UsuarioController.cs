using Modelo.General;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebSite.Controllers
{
	public class UsuarioController : Controller
	{
		// GET: Usuario
		public ActionResult Usuario()
		{
			return View();
		}

		public JsonResult MantenimientoUsuarios(UsuarioModelo usuario)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosUsuario().MantenimientoUsuarios(usuario);
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerUsuariosPorRol(int rolId)
		{
			List<UsuarioModelo> listaUsuarios = new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolId);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaUsuarios);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerCarreras()
		{
			List<CarreraModelo> listaUsuarios = new Negocios.NegociosUsuario().ObtenerCarreras();
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaUsuarios);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerRoles()
		{
			List<RolModelo> listaRoles = new Negocios.NegociosUsuario().ObtenerRoles();
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaRoles);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}
	}
}