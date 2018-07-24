using Modelo.General;
using Modelo.Mantenimiento;
using System;
using System.Collections.Generic;

namespace Negocios
{
	public class NegociosMantenimiento
	{
		public List<InformacionModelo> ObtenerInformacion(int tipo, int activo)
		{
			try
			{
				return new Datos.DatosMantenimiento().ObtenerInformacion(tipo, activo);
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
