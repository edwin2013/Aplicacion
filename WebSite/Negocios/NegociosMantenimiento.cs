using Modelo.General;
using Modelo.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocios
{
	public class NegociosMantenimiento
	{
		public List<InformacionModelo> ObtenerInformacionPorId(int informacionId)
		{
			try
			{
				return new Datos.DatosMantenimiento().ObtenerInformacionPorId(informacionId);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public List<InformacionModelo> ObtenerMultimediaInformacion(int tipo, int activo)
		{
			try
			{
				List<InformacionModelo> listaDatos = new Datos.DatosMantenimiento().ObtenerInformacion(tipo, activo);
				foreach (InformacionModelo item in listaDatos)
				{
					MultimediaInformacionModelo multimedia = new Datos.DatosMantenimiento().ObtenerMultimediaInformacion(item.InformacionId).FirstOrDefault();
					bool existeMultimedia = multimedia != null;
					if (existeMultimedia)
					{
						item.Multimedia = multimedia;
					}
				}

				return listaDatos;
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public List<InformacionModelo> ObtenerInformacion(int tipo, int activo)
		{
			try
			{
				List<InformacionModelo> listaDatos = new Datos.DatosMantenimiento().ObtenerInformacion(tipo, activo);
				return listaDatos;
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public List<MultimediaInformacionModelo> ObtenerMultimediaInformacion(int informacionId)
		{
			try
			{
				return new Datos.DatosMantenimiento().ObtenerMultimediaInformacion(informacionId);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public Mensaje MantenimientoInformacion(InformacionModelo informacion)
		{
			try
			{
				return new Datos.DatosMantenimiento().MantenimientoInformacion(informacion);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}

		public Mensaje MantenimientoMultimediaInformacion(MultimediaInformacionModelo multimedia)
		{
			try
			{
				return new Datos.DatosMantenimiento().MantenimientoMultimediaInformacion(multimedia);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}
	}
}
