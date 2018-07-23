using Datos;
using Modelo.General;
using Modelo.Practicante;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class NegociosPracticante
    {

        public CitaPracticanteModelo ObtenerCitasPorId(int citaId)
        {
            try
            {
                return new DatosPracticante().ObtenerCitasPorId(citaId);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public List<OfertaPracticante> ObtenerOfertaPracticante(FiltroCitas filtroCitas)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                return new DatosPracticante().ObtenerOfertaPracticante(filtroCitas);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public Mensaje MantenimientoCita(CitaPracticanteModelo citaModelo)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                mensaje = new DatosPracticante().MantenimientoCita(citaModelo);
            }
            catch (Exception excepcion)
            {
                mensaje.Exito = false;
                mensaje.Respuesta = excepcion.Message.ToString();
            }

            return mensaje;
        }

        public List<CitaPracticanteModelo> ObtenerCitasPracticante(FiltroCitas filtroCitas)
        {
            try
            {
                return new DatosPracticante().ObtenerCitasPracticante(filtroCitas);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        public Mensaje MantenimientoOfertaHorario(OfertaHorarioModelo ofertaHorario)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                mensaje = new DatosPracticante().MantenimientoOfertaHorario(ofertaHorario);
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
