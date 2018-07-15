using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebSite.Controllers
{
	public class UsuarioController : Controller
	{
		// GET: Usuario
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult ObtenerUsuariosPorRol(int rolId)
		{
			List<UsuarioModelo> listaUsuarios = new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolId);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;

			bool esRolPracticante = false;//TODO depende del rol
			int usuarioId = listaUsuarios.FirstOrDefault().UsuarioId;//TODO Id usuario logueado
			var datosEnviar = new
			{
				ListaUsuarios = listaUsuarios,
				EsRolPracticante = esRolPracticante,
				UsuarioId = usuarioId
			};
			var datos = new JavaScriptSerializer().Serialize(datosEnviar);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}
	}
}