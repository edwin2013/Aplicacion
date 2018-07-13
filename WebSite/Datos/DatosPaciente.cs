using Modelo.General;
using Modelo.ModeloMapeo;
using Modelo.Paciente;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Datos
{
	public class DatosPaciente
	{
		public List<DiasOfertaMes> ObtenerDiasOfertaMes(int mes, int anio)
		{
			List<DiasOfertaMes> listaDiasOferta = new List<DiasOfertaMes>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaDias = contexto.SP_ObtenerDiasOfertaMes(mes, anio);

				foreach (SP_ObtenerDiasOfertaMes_Result diaActual in listaDias)
				{
					DiasOfertaMes diaOfertaMes = new DiasOfertaMes();
					diaOfertaMes.DiaOferta = diaActual.DiaOferta ?? default(int);
					diaOfertaMes.DetalleDiaOferta = diaActual.DetalleDiaOferta;
					diaOfertaMes.FechaDiaOferta = diaActual.FechaDiaOferta;
					listaDiasOferta.Add(diaOfertaMes);
				}
			}

			return listaDiasOferta;
		}

		public List<SesionModelo> ObtenerSesionesActivas(string fechaDia)
		{
			List<SesionModelo> listaSesionesDisponibles = new List<SesionModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaHoras = contexto.SP_ObtenerSesionesActivas(fechaDia);

				foreach (SP_ObtenerSesionesActivas_Result horaActual in listaHoras)
				{
					SesionModelo sesion = new SesionModelo();
					sesion.Hora = horaActual.Hora ?? default(int);
					sesion.DetalleHora = horaActual.DetalleHora;
					listaSesionesDisponibles.Add(sesion);
				}
			}

			return listaSesionesDisponibles;
		}

		public Mensaje CrearCita(CitaModelo citaModelo)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_CrearCita(
					citaModelo.PacienteId,
					citaModelo.Dia,
					citaModelo.Hora,
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

		public Mensaje CrearPaciente(PacienteModelo pacienteModelo)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));
			ObjectParameter pacienteId = new ObjectParameter("PacienteId", typeof(int));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_CrearPaciente(
					pacienteModelo.Nombre,
					pacienteModelo.Apellidos,
					pacienteModelo.CorreoElectronico,
					pacienteModelo.Telefono,
					pacienteModelo.Nacionalidad,
					pacienteModelo.Identificacion,
					pacienteModelo.EstadoCivil,
					pacienteModelo.Edad,
					pacienteModelo.CantidadHijos,
					resultado,
					mensaje,
					pacienteId
					);
			}

			Mensaje mensajeMantenimiento =
				new Mensaje(
					Convert.ToBoolean(resultado.Value),
					Convert.ToString(mensaje.Value),
					Convert.ToInt32(pacienteId.Value
					));

			return mensajeMantenimiento;
		}
	}
}
