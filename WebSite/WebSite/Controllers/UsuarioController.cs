using Modelo.General;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public JsonResult MantenimientoUsuarios(UsuarioModelo usuario)
		{
            bool esAccionCrear = usuario.Accion == (char)Acciones.Ingreasar;
            usuario.Password = esAccionCrear ? new GeneracionCodigo().GenerarCodigoUnico() : usuario.Password;
            Mensaje mensajeRespuesta = new Negocios.NegociosUsuario().MantenimientoUsuarios(usuario);

            if (mensajeRespuesta.Exito && esAccionCrear)
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
            var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerUsuariosPorRol(int rolId)
		{
			List<UsuarioModelo> listaUsuarios = new Negocios.NegociosUsuario().ObtenerUsuariosPorRol(rolId);
			

            bool esRolPracticante = false;//TODO depende del usuario logueado
            int usuarioId = 1;//TODO depende del usuario logueado

            var data = new {
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