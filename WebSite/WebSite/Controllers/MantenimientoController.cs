using Modelo.General;
using Modelo.Mantenimiento;
using Negocios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utiles;

namespace WebSite.Controllers
{
	public class MantenimientoController : Controller
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
				byte[] data = null;
				HttpPostedFileBase file = Request.Files[0];
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				long numBytes = fileContent.Length;
				BinaryReader br = new BinaryReader(fileContent);
				data = br.ReadBytes((int)numBytes);
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

		public JsonResult Survey(MultimediaInformacionModelo multimedia)
		{
			byte[] data = null;
			HttpPostedFileBase file = Request.Files[0];
			int fileSize = file.ContentLength;
			string fileName = file.FileName;
			string mimeType = file.ContentType;
			System.IO.Stream fileContent = file.InputStream;
			long numBytes = fileContent.Length;
			BinaryReader br = new BinaryReader(fileContent);
			data = br.ReadBytes((int)numBytes);


			MultimediaInformacionModelo imagen = new MultimediaInformacionModelo();
			imagen.Accion = "I";
			imagen.Datos = data;
			Mensaje mensaje = new NegociosMantenimiento().MantenimientoMultimediaInformacion(imagen);

			using (MemoryStream ms = new MemoryStream(imagen.Datos))
			{
				System.Drawing.Image image = Image.FromStream(ms);
				string ruta = Server.MapPath("~/");
				image.Save(ruta + "UserPhoto.jpg");
			}

			return Json(new { Mensaje = mensaje });
		}
	}
}