using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Paciente
{
	public class PacienteModelo
	{
		public string Nombre { set; get; }
		public string Apellidos { set; get; }
		public string CorreoElectronico { set; get; }
		public string Telefono { set; get; }
		public string Nacionalidad { set; get; }
		public string Identificacion { set; get; }
		public int EstadoCivil { set; get; }
		public int Edad { set; get; }
		public int CantidadHijos { set; get; }
		public int PacienteId { set; get; }
		
	}
}
