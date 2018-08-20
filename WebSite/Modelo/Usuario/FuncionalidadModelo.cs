using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Usuario
{
    public class FuncionalidadModelo
    {
        public int FuncionalidadId { set; get; }
        public string Descripcion { set; get; }
        public int Identificador { set; get; }
        public string Nombre { set; get; }
        public string Tipo { set; get; }
        public bool Permiso { set; get; }
    }
}
