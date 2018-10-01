using Modelo.General;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }

        public ActionResult Salir()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login");
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
    }
}