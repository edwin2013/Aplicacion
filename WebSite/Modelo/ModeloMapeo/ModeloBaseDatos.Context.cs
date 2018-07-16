﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
    
        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<Cita> Cita { get; set; }
        public virtual DbSet<OfertaHorario> OfertaHorario { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Sesion> Sesion { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
    
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
    
        public virtual int SP_ActualizarPaciente(Nullable<int> pacienteId, string nombre, string apellidos, string correoElectronico, string telefono, string nacionalidad, string identificacion, Nullable<int> estadoCivil, Nullable<int> edad, Nullable<int> cantidadHijos, ObjectParameter resultado, ObjectParameter mensaje)
        {
            var pacienteIdParameter = pacienteId.HasValue ?
                new ObjectParameter("PacienteId", pacienteId) :
                new ObjectParameter("PacienteId", typeof(int));
    
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ActualizarPaciente", pacienteIdParameter, nombreParameter, apellidosParameter, correoElectronicoParameter, telefonoParameter, nacionalidadParameter, identificacionParameter, estadoCivilParameter, edadParameter, cantidadHijosParameter, resultado, mensaje);
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
    
        public virtual int SP_MantenimientoUsuarios(string accion, Nullable<int> usuarioId, string nombre, string apellidos, string identificacion, Nullable<int> rolId, string password, Nullable<int> carreraId, string inicioPractica, string finPractica, ObjectParameter resultado, ObjectParameter mensaje)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MantenimientoUsuarios", accionParameter, usuarioIdParameter, nombreParameter, apellidosParameter, identificacionParameter, rolIdParameter, passwordParameter, carreraIdParameter, inicioPracticaParameter, finPracticaParameter, resultado, mensaje);
        }
    }
}
