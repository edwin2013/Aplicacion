//------------------------------------------------------------------------------
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
    
    public partial class FUN_ObtenerCitas_Result
    {
        public Nullable<int> CitaId { get; set; }
        public Nullable<int> PacienteId { get; set; }
        public string Antecedentes { get; set; }
        public string Recomendaciones { get; set; }
        public Nullable<int> EstadoCita { get; set; }
        public Nullable<int> Calificacion { get; set; }
        public Nullable<long> SesionId { get; set; }
        public string IdentificadorGUID { get; set; }
    }
}