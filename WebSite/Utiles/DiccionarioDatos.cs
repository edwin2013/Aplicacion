using Modelo.Paciente;
using Modelo.Usuario;
using System.Collections.Generic;

namespace Utiles
{
	public class DiccionarioDatos
	{
		public Dictionary<string, string> CrearDiccionarioDatosPaciente(CrearCitaModelo citaModelo)
		{
			Dictionary<string, string> datosPaciente = new Dictionary<string, string>();
			datosPaciente.Add("NombrePaciente", citaModelo.PacienteModelo.Nombre + " " + citaModelo.PacienteModelo.Apellidos);
			datosPaciente.Add("CorreoElectronico", citaModelo.PacienteModelo.CorreoElectronico);
			datosPaciente.Add("Telefono", citaModelo.PacienteModelo.Telefono);
			datosPaciente.Add("Identificacion", citaModelo.PacienteModelo.Identificacion);
			datosPaciente.Add("HoraCita", citaModelo.CitaModelo.DetalleHora);
			datosPaciente.Add("FechaCita", citaModelo.CitaModelo.Dia);

			return datosPaciente;
		}

        public Dictionary<string, string> CrearDiccionarioCorreoCalificacion(string nombrePaciente, string urlpagina)
        {
            Dictionary<string, string> datosPaciente = new Dictionary<string, string>();
            datosPaciente.Add("NombrePaciente", nombrePaciente);
            datosPaciente.Add("UrlPagina", urlpagina);

            return datosPaciente;
        }

        public Dictionary<string, string> CrearDiccionarioNuevoUsuario(UsuarioModelo usuario, string urlpagina)
        {
            Dictionary<string, string> datosUsuario = new Dictionary<string, string>();
            datosUsuario.Add("NombreUsuario", usuario.Nombre + " " + usuario.Apellidos);
            datosUsuario.Add("Corrreo", usuario.Correo);
            datosUsuario.Add("Password", usuario.Password);
            datosUsuario.Add("UrlPagina", urlpagina);

            return datosUsuario;
        }
    }
}
