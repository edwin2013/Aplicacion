using Datos;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
	public class NegociosUsuario
	{
		public List<UsuarioModelo> ObtenerUsuariosPorRol(int rolId)
		{
			try
			{
				return new DatosUsuario().ObtenerUsuariosPorRol(rolId);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}
	}
}
