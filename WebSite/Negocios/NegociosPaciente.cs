﻿using Datos;
using Modelo.General;
using Modelo.Paciente;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class NegociosPaciente
    {
        public Mensaje ActualizarHorarioCita(CitaModelo citaModelo)
        {
            try
            {
                return new DatosPaciente().ActualizarHorarioCita(citaModelo);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public List<CitaModelo> ObtenerCitas(string identificadorGUID)
        {
            try
            {
                return new DatosPaciente().ObtenerCitas(identificadorGUID);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }
        public List<PacienteModelo> ObtenerPacientes(int pacienteId)
        {
            try
            {
                return new DatosPaciente().ObtenerPacientes(pacienteId);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public Mensaje ActualizarPaciente(PacienteModelo pacienteModelo)
        {
            try
            {
                return new DatosPaciente().ActualizarPaciente(pacienteModelo);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public List<DiasOfertaMes> ObtenerDiasOfertaMes(int mes, int anio)
        {
            try
            {
                return new DatosPaciente().ObtenerDiasOfertaMes(mes, anio);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public List<SesionModelo> ObtenerSesionesActivas(string dia)
        {
            try
            {
                return new DatosPaciente().ObtenerSesionesActivas(dia);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public Mensaje CrearCita(CrearCitaModelo crearCitaModelo)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                Mensaje mansajeCreacionPaciente = new DatosPaciente().CrearPaciente(crearCitaModelo.PacienteModelo);
                bool seCreoPacienteConExito = mansajeCreacionPaciente.Exito;

                if (seCreoPacienteConExito)
                {
                    crearCitaModelo.CitaModelo.PacienteId = mansajeCreacionPaciente.EntidadId;
                    mensaje = new DatosPaciente().CrearCita(crearCitaModelo.CitaModelo);
                }
                else
                {
                    mensaje = mansajeCreacionPaciente;
                }
            }
            catch (Exception excepcion)
            {
                mensaje.Exito = false;
                mensaje.Respuesta = excepcion.Message.ToString();
            }

            return mensaje;
        }
    }
}
