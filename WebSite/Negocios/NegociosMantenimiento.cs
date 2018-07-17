using Modelo.General;
using Modelo.Mantenimiento;
using System;

namespace Negocios
{
	public class NegociosMantenimiento
	{

		public Mensaje MantenimientoImagenActividades(ImagenActividades imagen)
		{
			try
			{
				return new Datos.DatosMantenimiento().MantenimientoImagenActividades(imagen);
			}
			catch (Exception excepcion)
			{
				throw new Exception(excepcion.Message);
			}
		}
	}
}
