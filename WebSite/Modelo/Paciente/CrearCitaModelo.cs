using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Paciente
{
	public class CrearCitaModelo
	{
        public CrearCitaModelo()
        {
            CitaModelo = new CitaModelo();
            PacienteModelo = new PacienteModelo();
        }

        public CitaModelo CitaModelo { set; get; }
		public PacienteModelo PacienteModelo { set; get; }
	}
}
