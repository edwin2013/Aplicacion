﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelo.ModeloMapeo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ManejoCitasEntities : DbContext
    {
        public ManejoCitasEntities()
            : base("name=ManejoCitasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int SP_MantenimientoOfertaHorario(string accion, Nullable<int> ofertaHorarioId, string dia, Nullable<int> horaInicio, Nullable<int> horaFin, Nullable<int> usuarioId, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var ofertaHorarioIdParameter = ofertaHorarioId.HasValue ?
                new ObjectParameter("OfertaHorarioId", ofertaHorarioId) :
                new ObjectParameter("OfertaHorarioId", typeof(int));
    
            var diaParameter = dia != null ?
                new ObjectParameter("Dia", dia) :
                new ObjectParameter("Dia", typeof(string));
    
            var horaInicioParameter = horaInicio.HasValue ?
                new ObjectParameter("HoraInicio", horaInicio) :
                new ObjectParameter("HoraInicio", typeof(int));
    
            var horaFinParameter = horaFin.HasValue ?
                new ObjectParameter("HoraFin", horaFin) :
                new ObjectParameter("HoraFin", typeof(int));
    
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoOfertaHorario", accionParameter, ofertaHorarioIdParameter, diaParameter, horaInicioParameter, horaFinParameter, usuarioIdParameter, resultado, mensaje);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerSesionesActivas")]
        public virtual IQueryable<Nullable<int>> FUN_ObtenerSesionesActivas(string dia)
        {
            var diaParameter = dia != null ?
                new ObjectParameter("Dia", dia) :
                new ObjectParameter("Dia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<int>>("[ManejoCitasEntities].[FUN_ObtenerSesionesActivas](@Dia)", diaParameter);
        }
    
        public virtual int SP_CrearPaciente(string nombre, string apellidos, string correoElectronico, string telefono, string nacionalidad, string identificacion, Nullable<int> estadoCivil, Nullable<int> edad, Nullable<int> cantidadHijos, ObjectParameter resultado, ObjectParameter mensaje, ObjectParameter pacienteId)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var nacionalidadParameter = nacionalidad != null ?
                new ObjectParameter("Nacionalidad", nacionalidad) :
                new ObjectParameter("Nacionalidad", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var estadoCivilParameter = estadoCivil.HasValue ?
                new ObjectParameter("EstadoCivil", estadoCivil) :
                new ObjectParameter("EstadoCivil", typeof(int));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("Edad", edad) :
                new ObjectParameter("Edad", typeof(int));
    
            var cantidadHijosParameter = cantidadHijos.HasValue ?
                new ObjectParameter("CantidadHijos", cantidadHijos) :
                new ObjectParameter("CantidadHijos", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CrearPaciente", nombreParameter, apellidosParameter, correoElectronicoParameter, telefonoParameter, nacionalidadParameter, identificacionParameter, estadoCivilParameter, edadParameter, cantidadHijosParameter, resultado, mensaje, pacienteId);
        }
    
        public virtual ObjectResult<SP_ObtenerDiasOfertaMes_Result> SP_ObtenerDiasOfertaMes(Nullable<int> mes, Nullable<int> anio)
        {
            var mesParameter = mes.HasValue ?
                new ObjectParameter("Mes", mes) :
                new ObjectParameter("Mes", typeof(int));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("Anio", anio) :
                new ObjectParameter("Anio", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerDiasOfertaMes_Result>("SP_ObtenerDiasOfertaMes", mesParameter, anioParameter);
        }
    
        public virtual ObjectResult<SP_ObtenerSesionesActivas_Result> SP_ObtenerSesionesActivas(string dia)
        {
            var diaParameter = dia != null ?
                new ObjectParameter("Dia", dia) :
                new ObjectParameter("Dia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerSesionesActivas_Result>("SP_ObtenerSesionesActivas", diaParameter);
        }
    
        public virtual ObjectResult<SP_ObtenerOfertaPracticante_Result> SP_ObtenerOfertaPracticante(Nullable<int> usuarioId, string fechaInicio, string fechaFin)
        {
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            var fechaInicioParameter = fechaInicio != null ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(string));
    
            var fechaFinParameter = fechaFin != null ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerOfertaPracticante_Result>("SP_ObtenerOfertaPracticante", usuarioIdParameter, fechaInicioParameter, fechaFinParameter);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerPacientes")]
        public virtual IQueryable<FUN_ObtenerPacientes_Result> FUN_ObtenerPacientes(Nullable<int> pacienteId)
        {
            var pacienteIdParameter = pacienteId.HasValue ?
                new ObjectParameter("PacienteId", pacienteId) :
                new ObjectParameter("PacienteId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerPacientes_Result>("[ManejoCitasEntities].[FUN_ObtenerPacientes](@PacienteId)", pacienteIdParameter);
        }
    
        public virtual int SP_CrearCita(Nullable<int> pacienteId, string dia, Nullable<int> hora, string identificadorGUID, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var pacienteIdParameter = pacienteId.HasValue ?
                new ObjectParameter("PacienteId", pacienteId) :
                new ObjectParameter("PacienteId", typeof(int));
    
            var diaParameter = dia != null ?
                new ObjectParameter("Dia", dia) :
                new ObjectParameter("Dia", typeof(string));
    
            var horaParameter = hora.HasValue ?
                new ObjectParameter("Hora", hora) :
                new ObjectParameter("Hora", typeof(int));
    
            var identificadorGUIDParameter = identificadorGUID != null ?
                new ObjectParameter("IdentificadorGUID", identificadorGUID) :
                new ObjectParameter("IdentificadorGUID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CrearCita", pacienteIdParameter, diaParameter, horaParameter, identificadorGUIDParameter, resultado, mensaje);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerCitas")]
        public virtual IQueryable<FUN_ObtenerCitas_Result> FUN_ObtenerCitas(string identificadorGUID)
        {
            var identificadorGUIDParameter = identificadorGUID != null ?
                new ObjectParameter("IdentificadorGUID", identificadorGUID) :
                new ObjectParameter("IdentificadorGUID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerCitas_Result>("[ManejoCitasEntities].[FUN_ObtenerCitas](@IdentificadorGUID)", identificadorGUIDParameter);
        }
    
        public virtual int SP_MantenimientoCita(string accion, Nullable<int> citaId, Nullable<int> calificacion, string antecedentes, string recomendaciones, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var citaIdParameter = citaId.HasValue ?
                new ObjectParameter("CitaId", citaId) :
                new ObjectParameter("CitaId", typeof(int));
    
            var calificacionParameter = calificacion.HasValue ?
                new ObjectParameter("Calificacion", calificacion) :
                new ObjectParameter("Calificacion", typeof(int));
    
            var antecedentesParameter = antecedentes != null ?
                new ObjectParameter("Antecedentes", antecedentes) :
                new ObjectParameter("Antecedentes", typeof(string));
    
            var recomendacionesParameter = recomendaciones != null ?
                new ObjectParameter("Recomendaciones", recomendaciones) :
                new ObjectParameter("Recomendaciones", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoCita", accionParameter, citaIdParameter, calificacionParameter, antecedentesParameter, recomendacionesParameter, resultado, mensaje);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerCarreras")]
        public virtual IQueryable<FUN_ObtenerCarreras_Result> FUN_ObtenerCarreras()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerCarreras_Result>("[ManejoCitasEntities].[FUN_ObtenerCarreras]()");
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerRoles")]
        public virtual IQueryable<FUN_ObtenerRoles_Result> FUN_ObtenerRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerRoles_Result>("[ManejoCitasEntities].[FUN_ObtenerRoles]()");
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerUsuariosPorRol")]
        public virtual IQueryable<FUN_ObtenerUsuariosPorRol_Result> FUN_ObtenerUsuariosPorRol(Nullable<int> rolId)
        {
            var rolIdParameter = rolId.HasValue ?
                new ObjectParameter("RolId", rolId) :
                new ObjectParameter("RolId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerUsuariosPorRol_Result>("[ManejoCitasEntities].[FUN_ObtenerUsuariosPorRol](@RolId)", rolIdParameter);
        }
    
        public virtual ObjectResult<SP_ObtenerCitasPracticante_Result> SP_ObtenerCitasPracticante(Nullable<int> usuarioId, string fechaInicio, string fechaFin, string apellidos, string ideintificacion)
        {
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            var fechaInicioParameter = fechaInicio != null ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(string));
    
            var fechaFinParameter = fechaFin != null ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var ideintificacionParameter = ideintificacion != null ?
                new ObjectParameter("Ideintificacion", ideintificacion) :
                new ObjectParameter("Ideintificacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerCitasPracticante_Result>("SP_ObtenerCitasPracticante", usuarioIdParameter, fechaInicioParameter, fechaFinParameter, apellidosParameter, ideintificacionParameter);
        }
    
        public virtual int SP_ActualizarHorarioCita(Nullable<int> citaId, string dia, Nullable<int> hora, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var citaIdParameter = citaId.HasValue ?
                new ObjectParameter("CitaId", citaId) :
                new ObjectParameter("CitaId", typeof(int));
    
            var diaParameter = dia != null ?
                new ObjectParameter("Dia", dia) :
                new ObjectParameter("Dia", typeof(string));
    
            var horaParameter = hora.HasValue ?
                new ObjectParameter("Hora", hora) :
                new ObjectParameter("Hora", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ActualizarHorarioCita", citaIdParameter, diaParameter, horaParameter, resultado, mensaje);
        }
    
        public virtual int SP_MantenimientoUsuarios(string accion, Nullable<int> usuarioId, string nombre, string apellidos, string identificacion, string correo, Nullable<int> rolId, string password, Nullable<int> carreraId, string inicioPractica, string finPractica, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var rolIdParameter = rolId.HasValue ?
                new ObjectParameter("RolId", rolId) :
                new ObjectParameter("RolId", typeof(int));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var carreraIdParameter = carreraId.HasValue ?
                new ObjectParameter("CarreraId", carreraId) :
                new ObjectParameter("CarreraId", typeof(int));
    
            var inicioPracticaParameter = inicioPractica != null ?
                new ObjectParameter("InicioPractica", inicioPractica) :
                new ObjectParameter("InicioPractica", typeof(string));
    
            var finPracticaParameter = finPractica != null ?
                new ObjectParameter("FinPractica", finPractica) :
                new ObjectParameter("FinPractica", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoUsuarios", accionParameter, usuarioIdParameter, nombreParameter, apellidosParameter, identificacionParameter, correoParameter, rolIdParameter, passwordParameter, carreraIdParameter, inicioPracticaParameter, finPracticaParameter, resultado, mensaje);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerUsuariosPorCredenciales")]
        public virtual IQueryable<FUN_ObtenerUsuariosPorCredenciales_Result> FUN_ObtenerUsuariosPorCredenciales(string correo, string password)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerUsuariosPorCredenciales_Result>("[ManejoCitasEntities].[FUN_ObtenerUsuariosPorCredenciales](@Correo, @Password)", correoParameter, passwordParameter);
        }
    
        public virtual ObjectResult<SP_ObtenerCitasPorId_Result> SP_ObtenerCitasPorId(Nullable<int> citaId)
        {
            var citaIdParameter = citaId.HasValue ?
                new ObjectParameter("CitaId", citaId) :
                new ObjectParameter("CitaId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerCitasPorId_Result>("SP_ObtenerCitasPorId", citaIdParameter);
        }
    
        public virtual int SP_MantenimientoActividades(string accion, Nullable<int> actividadId, string fecha, string titulo, Nullable<int> cupo, string descripcion, Nullable<bool> activo, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var actividadIdParameter = actividadId.HasValue ?
                new ObjectParameter("ActividadId", actividadId) :
                new ObjectParameter("ActividadId", typeof(int));
    
            var fechaParameter = fecha != null ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(string));
    
            var tituloParameter = titulo != null ?
                new ObjectParameter("Titulo", titulo) :
                new ObjectParameter("Titulo", typeof(string));
    
            var cupoParameter = cupo.HasValue ?
                new ObjectParameter("Cupo", cupo) :
                new ObjectParameter("Cupo", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoActividades", accionParameter, actividadIdParameter, fechaParameter, tituloParameter, cupoParameter, descripcionParameter, activoParameter, resultado, mensaje);
        }
    
        public virtual int SP_ActualizarPaciente(Nullable<int> pacienteId, Nullable<int> citaId, string nombre, string apellidos, string correoElectronico, string telefono, string nacionalidad, string identificacion, Nullable<int> estadoCivil, Nullable<int> edad, Nullable<int> cantidadHijos, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var pacienteIdParameter = pacienteId.HasValue ?
                new ObjectParameter("PacienteId", pacienteId) :
                new ObjectParameter("PacienteId", typeof(int));
    
            var citaIdParameter = citaId.HasValue ?
                new ObjectParameter("CitaId", citaId) :
                new ObjectParameter("CitaId", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var nacionalidadParameter = nacionalidad != null ?
                new ObjectParameter("Nacionalidad", nacionalidad) :
                new ObjectParameter("Nacionalidad", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var estadoCivilParameter = estadoCivil.HasValue ?
                new ObjectParameter("EstadoCivil", estadoCivil) :
                new ObjectParameter("EstadoCivil", typeof(int));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("Edad", edad) :
                new ObjectParameter("Edad", typeof(int));
    
            var cantidadHijosParameter = cantidadHijos.HasValue ?
                new ObjectParameter("CantidadHijos", cantidadHijos) :
                new ObjectParameter("CantidadHijos", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ActualizarPaciente", pacienteIdParameter, citaIdParameter, nombreParameter, apellidosParameter, correoElectronicoParameter, telefonoParameter, nacionalidadParameter, identificacionParameter, estadoCivilParameter, edadParameter, cantidadHijosParameter, resultado, mensaje);
        }
    
        public virtual int SP_ActualizarPassword(Nullable<int> usuarioId, string password, Nullable<bool> solicitarCambioPassword, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var solicitarCambioPasswordParameter = solicitarCambioPassword.HasValue ?
                new ObjectParameter("SolicitarCambioPassword", solicitarCambioPassword) :
                new ObjectParameter("SolicitarCambioPassword", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ActualizarPassword", usuarioIdParameter, passwordParameter, solicitarCambioPasswordParameter, resultado, mensaje);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerInformacion")]
        public virtual IQueryable<FUN_ObtenerInformacion_Result> FUN_ObtenerInformacion(Nullable<int> tipo, Nullable<int> activo)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(int));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerInformacion_Result>("[ManejoCitasEntities].[FUN_ObtenerInformacion](@Tipo, @Activo)", tipoParameter, activoParameter);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerMultimediaInformacion")]
        public virtual IQueryable<FUN_ObtenerMultimediaInformacion_Result> FUN_ObtenerMultimediaInformacion(Nullable<int> informacionId)
        {
            var informacionIdParameter = informacionId.HasValue ?
                new ObjectParameter("InformacionId", informacionId) :
                new ObjectParameter("InformacionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerMultimediaInformacion_Result>("[ManejoCitasEntities].[FUN_ObtenerMultimediaInformacion](@InformacionId)", informacionIdParameter);
        }
    
        public virtual int SP_MantenimientoMultimediaInformacion(string accion, Nullable<long> multimediaInformacionId, byte[] datos, string ruta, string nombre, string contentType, Nullable<int> informacionId, Nullable<int> tipo, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var multimediaInformacionIdParameter = multimediaInformacionId.HasValue ?
                new ObjectParameter("MultimediaInformacionId", multimediaInformacionId) :
                new ObjectParameter("MultimediaInformacionId", typeof(long));
    
            var datosParameter = datos != null ?
                new ObjectParameter("Datos", datos) :
                new ObjectParameter("Datos", typeof(byte[]));
    
            var rutaParameter = ruta != null ?
                new ObjectParameter("Ruta", ruta) :
                new ObjectParameter("Ruta", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var contentTypeParameter = contentType != null ?
                new ObjectParameter("ContentType", contentType) :
                new ObjectParameter("ContentType", typeof(string));
    
            var informacionIdParameter = informacionId.HasValue ?
                new ObjectParameter("InformacionId", informacionId) :
                new ObjectParameter("InformacionId", typeof(int));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoMultimediaInformacion", accionParameter, multimediaInformacionIdParameter, datosParameter, rutaParameter, nombreParameter, contentTypeParameter, informacionIdParameter, tipoParameter, resultado, mensaje);
        }
    
        [DbFunction("ManejoCitasEntities", "FUN_ObtenerInformacionPorId")]
        public virtual IQueryable<FUN_ObtenerInformacionPorId_Result> FUN_ObtenerInformacionPorId(Nullable<int> informacionId)
        {
            var informacionIdParameter = informacionId.HasValue ?
                new ObjectParameter("InformacionId", informacionId) :
                new ObjectParameter("InformacionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FUN_ObtenerInformacionPorId_Result>("[ManejoCitasEntities].[FUN_ObtenerInformacionPorId](@InformacionId)", informacionIdParameter);
        }
    
        public virtual int SP_MantenimientoInformacion(string accion, Nullable<int> informacionId, string fecha, string titulo, Nullable<int> cupo, string descripcion, Nullable<bool> activo, Nullable<int> tipo, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var informacionIdParameter = informacionId.HasValue ?
                new ObjectParameter("InformacionId", informacionId) :
                new ObjectParameter("InformacionId", typeof(int));
    
            var fechaParameter = fecha != null ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(string));
    
            var tituloParameter = titulo != null ?
                new ObjectParameter("Titulo", titulo) :
                new ObjectParameter("Titulo", typeof(string));
    
            var cupoParameter = cupo.HasValue ?
                new ObjectParameter("Cupo", cupo) :
                new ObjectParameter("Cupo", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoInformacion", accionParameter, informacionIdParameter, fechaParameter, tituloParameter, cupoParameter, descripcionParameter, activoParameter, tipoParameter, resultado, mensaje);
        }
    
        public virtual ObjectResult<SP_ObtenerPermisosUsuario_Result> SP_ObtenerPermisosUsuario(Nullable<int> usuarioId)
        {
            var usuarioIdParameter = usuarioId.HasValue ?
                new ObjectParameter("UsuarioId", usuarioId) :
                new ObjectParameter("UsuarioId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ObtenerPermisosUsuario_Result>("SP_ObtenerPermisosUsuario", usuarioIdParameter);
        }
    }
}
