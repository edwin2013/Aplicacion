//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Sesion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sesion()
        {
            this.Cita = new HashSet<Cita>();
        }
    
        public long SesionId { get; set; }
        public Nullable<int> OfertaHorarioId { get; set; }
        public Nullable<int> Hora { get; set; }
        public Nullable<bool> Ocupado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual OfertaHorario OfertaHorario { get; set; }
    }
}