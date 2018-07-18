using Modelo.General;
using Modelo.Mantenimiento;
using Negocios;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
	public class MantenimientoController : Controller
	{
		public ActionResult Mantenimiento()
		{
			return View();
		}

		public JsonResult Survey()
		{
			byte[] data = null;

			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i]; //Uploaded file
															//Use the following properties to get file's name, size and MIMEType
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				long numBytes = fileContent.Length;
				BinaryReader br = new BinaryReader(fileContent);
				data = br.ReadBytes((int)numBytes);
			}

			ImagenActividades imagen = new ImagenActividades();
			imagen.Accion = "I";
			imagen.Datos = Encoding.UTF8.GetString(data); ;
			//byte[] theBytes = Encoding.UTF8.GetBytes(theString);
			Mensaje mensaje = new NegociosMantenimiento().MantenimientoImagenActividades(imagen);

			return Json(new { Mensaje = mensaje });
		}

		public JsonResult Upload()
		{
			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i]; //Uploaded file
															//Use the following properties to get file's name, size and MIMEType
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				//To save file, use SaveAs method
				file.SaveAs(Server.MapPath("~/") + fileName); //File will be saved in application root
			}
			return Json("Uploaded " + Request.Files.Count + " files");
		}
	}
}