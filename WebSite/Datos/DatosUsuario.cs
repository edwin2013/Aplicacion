using Modelo.ModeloMapeo;
using Modelo.Usuario;
using System;
using System.Collections.Generic;

namespace Datos
{
	public class DatosUsuario
	{
		public List<UsuarioModelo> ObtenerUsuariosPorRol(int rolId)
		{
			List<UsuarioModelo> listaUsuarios = new List<UsuarioModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaDiasConsulta = contexto.FUN_ObtenerUsuariosPorRol(rolId);

				foreach (FUN_ObtenerUsuariosPorRol_Result usuarioActual in listaDiasConsulta)
				{
					UsuarioModelo usuario = new UsuarioModelo();
					usuario.UsuarioId = usuarioActual.UsuarioId ?? default(int);
					usuario.Nombre = usuarioActual.Nombre;
					usuario.Apellidos = usuarioActual.Apellidos;
					usuario.Identificacion = usuarioActual.Identificacion;
					usuario.RolId = usuarioActual.RolId ?? default(int);
					usuario.Password = usuarioActual.Password;
					usuario.CarreraId = usuarioActual.CarreraId ?? default(int);
					usuario.InicioPractica = usuarioActual.InicioPractica ?? default(DateTime);
					usuario.FinPractica = usuarioActual.FinPractica ?? default(DateTime);
					listaUsuarios.Add(usuario);
				}
			}

			return listaUsuarios;
		}
	}
}
