using Modelo.General;
using Modelo.Mantenimiento;
using Modelo.ModeloMapeo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Datos
{
	public class DatosMantenimiento
	{
		public Mensaje MantenimientoInformacion(InformacionModelo informacion)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoInformacion(
					informacion.Accion,
					informacion.InformacionId,
					informacion.Fecha,
					informacion.Titulo,
					informacion.Cupo,
					informacion.Descripcion,
					informacion.Activo,
					informacion.Tipo,
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

		public Mensaje MantenimientoMultimediaInformacion(MultimediaInformacionModelo multimedia)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoMultimediaInformacion(
					multimedia.Accion,
					multimedia.MultimediaInformacionId,
					multimedia.Datos,
					multimedia.Ruta,
					multimedia.InformacionId,
					multimedia.Tipo,
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

		public List<MultimediaInformacionModelo> ObtenerMultimediaInformacion(int informacionId)
		{
			List<MultimediaInformacionModelo> listaRetornar = new List<MultimediaInformacionModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var lista = contexto.FUN_ObtenerMultimediaInformacion(informacionId);

				foreach (FUN_ObtenerMultimediaInformacion_Result item in lista)
				{
					MultimediaInformacionModelo entidad = new MultimediaInformacionModelo();
					entidad.MultimediaInformacionId = item.MultimediaInformacionId ?? default(Int64);
					entidad.Datos = item.Datos;
					entidad.Ruta = item.Ruta;
					entidad.InformacionId = item.InformacionId ?? default(int);
					entidad.Tipo = item.Tipo ?? default(int);
					listaRetornar.Add(entidad);
				}
			}

			return listaRetornar;
		}

		public List<InformacionModelo> ObtenerInformacion(int tipo, int activo)
		{
			List<InformacionModelo> listaRetornar = new List<InformacionModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var lista = contexto.FUN_ObtenerInformacion(tipo, activo);

				foreach (FUN_ObtenerInformacion_Result item in lista)
				{
					InformacionModelo entidad = new InformacionModelo();
					entidad.InformacionId = item.InformacionId ?? default(int);
					entidad.Fecha = item.Fecha;
					entidad.Titulo = item.Titulo;
					entidad.Cupo = item.Cupo ?? default(int);
					entidad.Descripcion = item.Descripcion;
					entidad.Activo = item.Activo ?? default(bool); ;
					entidad.Tipo = item.Tipo ?? default(int);
					listaRetornar.Add(entidad);
				}
			}

			return listaRetornar;
		}
	}
}
