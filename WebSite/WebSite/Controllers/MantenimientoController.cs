using Modelo.General;
using Modelo.Mantenimiento;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;

namespace WebSite.Controllers
{
	public class MantenimientoController : BaseController
    {
		public ActionResult Mantenimiento()
		{
			return View();
		}

		public JsonResult MantenimientoInformacion(InformacionModelo informacion)
		{
			Mensaje mensajeRespuesta = new Negocios.NegociosMantenimiento().MantenimientoInformacion(informacion);
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult MantenimientoMultimediaInformacion(MultimediaInformacionModelo multimedia)
		{
			bool esTipoImagen = multimedia.Tipo == (int)Archivos.Imagen;

			if (esTipoImagen)
			{
				HttpPostedFileBase file = Request.Files[0];
				int fileSize = file.ContentLength;
				multimedia.Nombre = file.FileName;
				multimedia.ContentType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				long numBytes = fileContent.Length;
				BinaryReader br = new BinaryReader(fileContent);
				byte[] data = br.ReadBytes((int)numBytes);
				multimedia.Datos = data;
			}

			Mensaje mensajeRespuesta = new Negocios.NegociosMantenimiento().MantenimientoMultimediaInformacion(multimedia);
			var datos = new JavaScriptSerializer().Serialize(mensajeRespuesta);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerInformacion(int tipo, int activo)
		{
			List<InformacionModelo> listaRetornar = new Negocios.NegociosMantenimiento().ObtenerInformacion(tipo, activo);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaRetornar);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerMultimediaInformacion(int informacionId)
		{
			List<MultimediaInformacionModelo> listaRetornar = new Negocios.NegociosMantenimiento().ObtenerMultimediaInformacion(informacionId);
			JavaScriptSerializer seralizador = new JavaScriptSerializer();
			seralizador.MaxJsonLength = Int32.MaxValue;
			var datos = new JavaScriptSerializer().Serialize(listaRetornar);
			return Json(datos, JsonRequestBehavior.AllowGet);
		}


		public ActionResult MostrarMultimedia(int informacionId)
		{
			var vista = (string)null;
			string mensajeError = string.Empty;
			bool poseeDatos = false;
			var datos = new { mensajeError = "", vistaHtml = "", poseeDatos = false };

			List<MultimediaInformacionModelo> lista = new List<MultimediaInformacionModelo>(); ;
			try
			{
				lista = new Negocios.NegociosMantenimiento().ObtenerMultimediaInformacion(informacionId);
				poseeDatos = lista.Count > 0;
			}
			catch (Exception excepcion)
			{
				mensajeError = "Ocurrio un error consultando los datos.";
			}

			vista = ConvertirVistaString("/Views/Mantenimiento/_MultimediaInformacion.cshtml", lista);

			JsonSerializerSettings jsSettings = new JsonSerializerSettings();
			jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			datos = new
			{
				mensajeError = mensajeError,
				vistaHtml = vista,
				poseeDatos = poseeDatos
			};

			var converted = JsonConvert.SerializeObject(datos, null, jsSettings);
			return Content(converted, "application/json");
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