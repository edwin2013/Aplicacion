using Modelo.General;
using Modelo.ModeloMapeo;
using Modelo.Practicante;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;


namespace Datos
{
	public class DatosPracticante
	{
		public List<OfertaPracticante> ObtenerOfertaPracticante(FiltroCitas filtroCitas)
		{
			List<OfertaPracticante> listaOfertaPracticante = new List<OfertaPracticante>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaOfertas = contexto.SP_ObtenerOfertaPracticante(filtroCitas.UsuarioId, filtroCitas.FechaInicio, filtroCitas.FechaFin);

				foreach (SP_ObtenerOfertaPracticante_Result ofertaActual in listaOfertas)
				{
					OfertaPracticante oferta = new OfertaPracticante();
					oferta.OfertaHorarioId = ofertaActual.OfertaHorarioId;
					oferta.DiaOferta = ofertaActual.DiaOferta;
					oferta.HoraInicio = ofertaActual.HoraInicio;
					oferta.HoraFin = ofertaActual.HoraFin;
					oferta.Practicante = ofertaActual.Practicante;
					oferta.InicioPractica = ofertaActual.InicioPractica;
					oferta.FinPractica = ofertaActual.FinPractica;
					oferta.Carrera = ofertaActual.Carrera;
					oferta.PoseeCitas = ofertaActual.PoseeCitas ?? default(bool);

					listaOfertaPracticante.Add(oferta);
				}
			}

			return listaOfertaPracticante;
		}

		public List<CitaPracticanteModelo> ObtenerCitasPracticante(FiltroCitas filtroCitas)
		{
			List<CitaPracticanteModelo> listaCitasPracticante = new List<CitaPracticanteModelo>();

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				var listaCitas = contexto.SP_ObtenerCitasPracticante(filtroCitas.UsuarioId, filtroCitas.FechaInicio, filtroCitas.FechaFin, filtroCitas.Apellidos, filtroCitas.Identificacion);

				foreach (SP_ObtenerCitasPracticante_Result citaActual in listaCitas)
				{
					CitaPracticanteModelo cita = new CitaPracticanteModelo();
					cita.HoraCita = citaActual.HoraCita;
                    cita.HoraEntero = citaActual.HoraEntero ?? default(int);
					cita.CitaId = citaActual.CitaId;
					cita.PacienteId = citaActual.PacienteId;
					cita.EstadoCita = citaActual.EstadoCita;
					cita.FechaCita = citaActual.FechaCita;
					cita.Practicante = citaActual.Practicante;
					cita.Paciente = citaActual.Paciente;
					cita.Identificacion = citaActual.Identificacion;
					cita.Telefono = citaActual.Telefono;
					cita.CorreoElectronico = citaActual.CorreoElectronico;
					cita.CantidadHijos = citaActual.CantidadHijos ?? default(int);
					cita.EstadoCivil = citaActual.EstadoCivil;
					cita.Recomendaciones = citaActual.Recomendaciones;
					cita.Antecedentes = citaActual.Antecedentes;
                    cita.IdentificadorGUID = citaActual.IdentificadorGUID;
					listaCitasPracticante.Add(cita);
				}
			}

			return listaCitasPracticante;
		}

		public Mensaje MantenimientoOfertaHorario(OfertaHorarioModelo ofertaHorario)
		{

			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoOfertaHorario(
					ofertaHorario.Accion,
					ofertaHorario.OfertaHorarioId,
					ofertaHorario.Dia,
					ofertaHorario.HoraInicio,
					ofertaHorario.HoraFin,
					ofertaHorario.UsuarioId,
					resultado,
					mensaje
					);
			}

			Mensaje mensajeMantenimiento = new Mensaje(Convert.ToBoolean(resultado.Value), Convert.ToString(mensaje.Value));
			return mensajeMantenimiento;
		}

		public Mensaje MantenimientoCita(CitaPracticanteModelo citaModelo)
		{
			ObjectParameter resultado = new ObjectParameter("Resultado", typeof(bool));
			ObjectParameter mensaje = new ObjectParameter("Mensaje", typeof(string));

			using (ManejoCitasEntities contexto = new ManejoCitasEntities())
			{
				contexto.SP_MantenimientoCita(
					citaModelo.Accion.ToString(),
					citaModelo.CitaId,
                    citaModelo.Calificacion,
					citaModelo.Antecedentes,
					citaModelo.Recomendaciones,
					resultado,
					mensaje
					);
			}

			Mensaje mensajeMantenimiento = new Mensaje(Convert.ToBoolean(resultado.Value), Convert.ToString(mensaje.Value));
			return mensajeMantenimiento;
		}
	}
}
