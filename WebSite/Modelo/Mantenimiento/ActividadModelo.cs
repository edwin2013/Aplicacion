using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Mantenimiento
{
    public class ActividadModelo
    {
        public int ActividadId { set; get; }
        public string Fecha { set; get; }
        public string Titulo { set; get; }
        public int Cupo { set; get; }
        public string Descripcion { set; get; }
        public bool Activo { set; get; }
    }
}
