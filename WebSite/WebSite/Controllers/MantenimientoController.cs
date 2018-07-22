using Modelo.General;
using Modelo.Mantenimiento;
using Negocios;
using System.Drawing;
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
				HttpPostedFileBase file = Request.Files[i]; 
															
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
			imagen.Datos = data; 
			Mensaje mensaje = new NegociosMantenimiento().MantenimientoImagenActividades(imagen);

            using (MemoryStream ms = new MemoryStream(imagen.Datos))
            {
                System.Drawing.Image image = Image.FromStream(ms);
                string ruta = Server.MapPath("~/");
                image.Save( ruta + "UserPhoto.jpg");
            }

            return Json(new { Mensaje = mensaje });
		}

		public JsonResult Upload()
		{
			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i]; 
															
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				
				file.SaveAs(Server.MapPath("~/") + fileName);
			}
			return Json("Uploaded " + Request.Files.Count + " files");
		}
	}
}