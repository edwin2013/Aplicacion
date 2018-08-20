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
	public class UsuarioController : Controller
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

		public JsonResult RecuperarPassword(string correo)
		{
			UsuarioModelo usuario =
			new Negocios.NegociosUsuario().ObtenerUsuariosPorCredenciales(correo, "-1").FirstOrDefault();
			bool existeUsuario = usuario != null;

			Mensaje mensajeRespuesta = new Mensaje();
			if (existeUsuario)
			{
				string passwordGenerado = new GeneracionCodigo().GenerarCodigoUnico();
				bool solicitarCambioPassword = true; 
				mensajeRespuesta =
				new Negocios.NegociosUsuario().ActualizarPassword(usuario.UsuarioId, passwordGenerado, solicitarCambioPassword);

				if (mensajeRespuesta.Exito)
				{
					EnvioCorreoNuevoUsuario(usuario);
					mensajeRespuesta.Respuesta = "Se ha enviado un link al correo para recuperar el password.";
				}
			}
			else
			{
				mensajeRespuesta.Exito = false;
				mensajeRespuesta.Respuesta = "El correo no esta asociado a ningún usuario del sistema.";
			}

			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = seralizador.Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
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

		public JsonResult ValidarCredenciales(string correo, string password)
		{
			Mensaje mensajeRespuesta = new Mensaje();
			UsuarioModelo usuario =
			new Negocios.NegociosUsuario().ObtenerUsuariosPorCredenciales(correo, password).FirstOrDefault();
			bool existeUsuario = usuario != null;
			mensajeRespuesta.Exito = existeUsuario;
			mensajeRespuesta.SolicitarCambioPassword = existeUsuario ? usuario.SolicitarCambioPassword : false;
			mensajeRespuesta.Respuesta = existeUsuario ? "Usuario autenticado exitosamente" : "Correo o passward inválido";

			if (mensajeRespuesta.Exito)
			{
				Session["usuario"] = usuario;
			}

			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = seralizador.Serialize(mensajeRespuesta);
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


			bool esRolPracticante = false;//TODO depende del usuario logueado
			int usuarioId = 1;//TODO depende del usuario logueado

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