using Modelo.Paciente;
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
	}
}
