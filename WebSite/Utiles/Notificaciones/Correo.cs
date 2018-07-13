using Modelo.General;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Utiles.Notificaciones
{
	public class Correo
	{
		public void EnviarCorreoAsincrono(DatosCorreo datosCorreo)
		{
			try
			{
				EnviarCorreoConHtml_Delegate delegado = new EnviarCorreoConHtml_Delegate(EnviarCorreo);
				delegado.BeginInvoke(datosCorreo, null, null);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public delegate void EnviarCorreoConHtml_Delegate(DatosCorreo correoEntidad);

		public void EnviarCorreo(DatosCorreo datosCorreo)
		{
			try
			{
				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient(datosCorreo.SmtpCliente);
				mail.From = new MailAddress(datosCorreo.CorreoEmisor, datosCorreo.NombreRemitente, Encoding.UTF8);
				mail.Subject = datosCorreo.Asunto;
				mail.Body = datosCorreo.CuerpoHTML;
				mail.IsBodyHtml = true;
				mail.Priority = MailPriority.High;

				if (datosCorreo.EsCorreoMultiDestinatario)
				{
					foreach (string correo in datosCorreo.MultiplesDestinatarios)
					{
						mail.To.Add(correo.ToLower());
					}
				}
				else
				{
					mail.To.Add(datosCorreo.CorreoReceptor.ToLower());
				}

				bool correoAEnviarConCopiaNoEstaVacio = datosCorreo.CorreoAEnviarConCopia != string.Empty;
				if (correoAEnviarConCopiaNoEstaVacio)
				{
					MailAddress copy = new MailAddress(datosCorreo.CorreoAEnviarConCopia.ToLower());
					mail.CC.Add(copy);
				}

				bool correosAEnviarConCopiaNoEstaVacio = datosCorreo.ListadoCorreosAEnviarConCopia.Count > 0;
				if (correosAEnviarConCopiaNoEstaVacio)
				{
					foreach (string correoCc in datosCorreo.ListadoCorreosAEnviarConCopia)
					{
						MailAddress copy = new MailAddress(correoCc.ToLower());
						mail.CC.Add(copy);
					}
				}

				bool correoAEnviarConCopiaOcultaNoEstaVacio = datosCorreo.CorreoAEnviarConCopia != string.Empty;
				if (correoAEnviarConCopiaOcultaNoEstaVacio)
				{
					MailAddress cco = new MailAddress(datosCorreo.CorreoAEnviarConCopiaOculta.ToLower());
					mail.Bcc.Add(cco);
				}

				bool existeArchivosAdjuntos = datosCorreo.MultiplesRutasDeArchivosAdjuntos.Count > 0;
				if (existeArchivosAdjuntos)
				{
					foreach (var rutaArchivo in datosCorreo.MultiplesRutasDeArchivosAdjuntos)
					{
						Attachment archivoSubir = new Attachment(rutaArchivo);
						archivoSubir.Name = Path.GetFileName(rutaArchivo);
						mail.Attachments.Add(archivoSubir);
					}
				}

				SmtpServer.Port = Convert.ToInt32(datosCorreo.PuertoCorreo);
				SmtpServer.UseDefaultCredentials = false;
				SmtpServer.Credentials = new System.Net.NetworkCredential(datosCorreo.CorreoEmisor, datosCorreo.ClaveCorreo);
				SmtpServer.EnableSsl = true;
				SmtpServer.Send(mail);
				mail.Dispose();
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

	}
}
