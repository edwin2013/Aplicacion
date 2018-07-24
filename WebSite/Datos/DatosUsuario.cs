using Modelo.General;
using Modelo.ModeloMapeo;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Datos
{
	public class DatosUsuario
	{
		public Mensaje ActualizarPassword(int usuarioId, string password, bool solicitarCambioPassword)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_ActualizarPassword(
					usuarioId,
					password,
					solicitarCambioPassword,
					resultado,
					mensaje
					);
			}

			Mensaje mensajeMantenimiento =
				new Mensaje(
					Convert.ToBoolean(resultado.Value),
					Convert.ToString(mensaje.Value));

			return mensajeMantenimiento;
		}

		public List<UsuarioModelo> ObtenerUsuariosPorCredenciales(string correo, string password)
		{
			List<UsuarioModelo> listaUsuarios = new List<UsuarioModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaDiasConsulta = contexto.FUN_ObtenerUsuariosPorCredenciales(correo, password);

				foreach (FUN_ObtenerUsuariosPorCredenciales_Result usuarioActual in listaDiasConsulta)
				{
					UsuarioModelo usuario = new UsuarioModelo();
					usuario.UsuarioId = usuarioActual.UsuarioId ?? default(int);
					usuario.Nombre = usuarioActual.Nombre;
					usuario.Apellidos = usuarioActual.Apellidos;
					usuario.DescripcionRol = usuarioActual.DescripcionRol;
					usuario.Identificacion = usuarioActual.Identificacion;
					usuario.RolId = usuarioActual.RolId ?? default(int);
					usuario.Password = usuarioActual.Password;
					usuario.CarreraId = usuarioActual.CarreraId ?? default(int);
					usuario.InicioPractica = usuarioActual.InicioPractica;
					usuario.FinPractica = usuarioActual.FinPractica;
					usuario.Correo = usuarioActual.Correo;
					usuario.SolicitarCambioPassword = usuarioActual.SolicitarCambioPassword ?? default(bool);
					listaUsuarios.Add(usuario);
				}
			}

			return listaUsuarios;
		}

		public Mensaje MantenimientoUsuarios(UsuarioModelo usuario)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoUsuarios(
					usuario.Accion.ToString(),
					usuario.UsuarioId,
					usuario.Nombre,
					usuario.Apellidos,
					usuario.Identificacion,
					usuario.Correo,
					usuario.RolId,
					usuario.Password,
					usuario.CarreraId,
					usuario.InicioPractica,
					usuario.FinPractica,
					resultado,
					mensaje
					);
			}

			Mensaje mensajeMantenimiento =
				new Mensaje(
					Convert.ToBoolean(resultado.Value),
					Convert.ToString(mensaje.Value));

			return mensajeMantenimiento;
		}

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
					usuario.DescripcionRol = usuarioActual.DescripcionRol;
					usuario.Identificacion = usuarioActual.Identificacion;
					usuario.RolId = usuarioActual.RolId ?? default(int);
					usuario.Password = usuarioActual.Password;
					usuario.CarreraId = usuarioActual.CarreraId ?? default(int);
					usuario.InicioPractica = usuarioActual.InicioPractica;
					usuario.FinPractica = usuarioActual.FinPractica;
					usuario.Correo = usuarioActual.Correo;
					listaUsuarios.Add(usuario);
				}
			}

			return listaUsuarios;
		}

		public List<RolModelo> ObtenerRoles()
		{
			List<RolModelo> listaRoles = new List<RolModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var roles = contexto.FUN_ObtenerRoles();

				foreach (FUN_ObtenerRoles_Result rolActual in roles)
				{
					RolModelo rol = new RolModelo();
					rol.RolId = rolActual.RolId ?? default(int);
					rol.Descripcion = rolActual.Descripcion;
					listaRoles.Add(rol);
				}
			}

			return listaRoles;
		}

		public List<CarreraModelo> ObtenerCarreras()
		{
			List<CarreraModelo> listaCarreras = new List<CarreraModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var carreras = contexto.FUN_ObtenerCarreras();

				foreach (FUN_ObtenerCarreras_Result carreraActual in carreras)
				{
					CarreraModelo carrera = new CarreraModelo();
					carrera.CarreraId = carreraActual.CarreraId ?? default(int);
					carrera.Nombre = carreraActual.Nombre;
					listaCarreras.Add(carrera);
				}
			}

			return listaCarreras;
		}
	}
}
