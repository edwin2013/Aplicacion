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
        public List<ActividadModelo> ObtenerRoles()
        {
            List<ActividadModelo> listaRetornar = new List<ActividadModelo>();

            using (ManejoCitasEntities contexto = new ManejoCitasEntities())
            {
                var lista = contexto.FUN_ObtenerActividades();

                foreach (FUN_ObtenerActividades_Result item in lista)
                {
                    ActividadModelo actividad = new ActividadModelo();
                    //rol.RolId = item.RolId ?? default(int);
                    //rol.Descripcion = item.Descripcion;
                    listaRetornar.Add(actividad);
                }
            }

            return listaRetornar;
        }

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
