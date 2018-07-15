using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Paciente
{
    public class CitaModelo
    {
        public int CitaId { get; set; }
        public int PacienteId { get; set; }
        public string Antecedentes { get; set; }
        public string Recomendaciones { get; set; }
        public int EstadoCita { get; set; }
        public int Calificacion { get; set; }
        public int SesionId { get; set; }
        public string Dia { get; set; }
        public int Hora { get; set; }
        public string DetalleHora { get; set; }
        public string IdentificadorGUID { get; set; }
    }
}
