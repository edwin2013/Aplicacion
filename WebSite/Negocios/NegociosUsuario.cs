using Datos;
using Modelo.General;
using Modelo.Usuario;
using System;
using System.Collections.Generic;

namespace Negocios
{
	public class NegociosUsuario
	{
        public List<UsuarioModelo> ObtenerUsuariosPorCredenciales(string correo, string password)
        {
            try
            {
                return new DatosUsuario().ObtenerUsuariosPorCredenciales(correo, password);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public Mensaje MantenimientoUsuarios(UsuarioModelo usuario)
		{
			try
			{
				return new DatosUsuario().MantenimientoUsuarios(usuario);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

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

		public List<RolModelo> ObtenerRoles()
		{
			try
			{
				return new DatosUsuario().ObtenerRoles();
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public List<CarreraModelo> ObtenerCarreras()
		{
			try
			{
				return new DatosUsuario().ObtenerCarreras();
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}
	}
}
