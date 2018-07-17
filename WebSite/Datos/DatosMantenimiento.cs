using Modelo.General;
using Modelo.Mantenimiento;
using Modelo.ModeloMapeo;
using System;
using System.Data.Entity.Core.Objects;

namespace Datos
{
	public class DatosMantenimiento
	{
		public Mensaje MantenimientoImagenActividades(ImagenActividades imagen)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoImagenActividades(
					imagen.Accion,
					imagen.ImagenId,
					imagen.Datos,
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

	}
}
