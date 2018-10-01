using Modelo.General;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;
using WebSite.Models;

namespace WebSite.Controllers
{
	public class UsuarioController : BaseController
    {
		// GET: Usuario
		public ActionResult Usuario()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		public JsonResult ActualizarPassword(string password)
		{
			Mensaje mensajeRespuesta = new Mensaje();
			UsuarioModelo usuarioSesion = new UsuarioModelo();
			bool existeSesion = Session["usuario"] != null;
			usuarioSesion = existeSesion ? Session["usuario"] as UsuarioModelo : usuarioSesion;

			if (existeSesion)
			{
				bool solicitarCambioPassword = false;
				mensajeRespuesta =
				new Negocios.NegociosUsuario().ActualizarPassword(usuarioSesion.UsuarioId, password, solicitarCambioPassword);

				if (mensajeRespuesta.Exito)
				{
					((UsuarioModelo)Session["usuario"]).SolicitarCambioPassword = solicitarCambioPassword;
				}
			}

			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = seralizador.Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ConsultarUsuarioSesion()
		{
			UsuarioModelo usuarioSesion = new UsuarioModelo();
			bool existeSesion = Session["usuario"] != null;
			usuarioSesion = existeSesion ? Session["usuario"] as UsuarioModelo : usuarioSesion;

            JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = seralizador.Serialize(usuarioSesion);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult MantenimientoUsuarios(UsuarioModelo usuario)
		{
			bool esAccionCrear = usuario.Accion == (char)Acciones.Ingreasar;
			bool esAccionActualizar = usuario.Accion == (char)Acciones.Actualizar;
			bool esAccionEliminar = usuario.Accion == (char)Acciones.Eliminar;
			Mensaje mensajeRespuesta = new Mensaje();

			UsuarioModelo usuarioConsultar =
			new Negocios.NegociosUsuario().ObtenerUsuariosPorCredenciales(usuario.Correo, "-1").FirstOrDefault();
			bool correoEstaDisponible =
			esAccionCrear ? (usuarioConsultar == null) :
			esAccionActualizar ? (usuario.UsuarioId == usuarioConsultar.UsuarioId) : esAccionEliminar;

			if (correoEstaDisponible)
			{
				usuario.Password = esAccionCrear ? new GeneracionCodigo().GenerarCodigoUnico() : usuario.Password;
				mensajeRespuesta = new Negocios.NegociosUsuario().MantenimientoUsuarios(usuario);
			}
			else
			{
				mensajeRespuesta.Exito = false;
				mensajeRespuesta.Respuesta = "El correo ya se encuentra en uso por un usuario del sistema.";
			}

			if (mensajeRespuesta.Exito && esAccionCrear)
			{
				EnvioCorreoNuevoUsuario(usuario);
			}

			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public void EnvioCorreoNuevoUsuario(UsuarioModelo usuario)
		{
			string asunto = ConfigurationManager.AppSettings["asuntoCorreoNuevoUsuario"];
			ManejadorCorreos manejadorCorreos = new Models.ManejadorCorreos(usuario.Correo, asunto);
			string urlPaginaLogin =
			System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Usuario/Login";
			Dictionary<string, string> datosCorreo = new DiccionarioDatos().CrearDiccionarioNuevoUsuario(usuario, urlPaginaLogin);
			string rutaServer = Server.MapPath("~/");
			string rutaPlantilla = rutaServer + ConfigurationManager.AppSettings["rutaPlantillaNuevoUsuario"];
			manejadorCorreos.CrearCuerpoCorreo(rutaPlantilla, datosCorreo);
			manejadorCorreos.EnviarCorreo();

		}

		public JsonResult ObtenerUsuariosPorRol(int rolId)
		{
			List<UsuarioModelo> listaUsuarios = new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolId);

            UsuarioModelo usuario = Session["usuario"] as UsuarioModelo;
			bool esRolPracticante = usuario.RolId == (int)Roles.Practicante;
			int usuarioId = usuario.UsuarioId;

			var data = new
			{
				ListaUsuarios = listaUsuarios,
				EsRolPracticante = esRolPracticante,
				UsuarioId = usuarioId
			};

			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = seralizador.Serialize(data);
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