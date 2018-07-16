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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.OfertaHorario = new HashSet<OfertaHorario>();
        }
    
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public Nullable<int> RolId { get; set; }
        public string Password { get; set; }
        public Nullable<int> Carrera { get; set; }
        public Nullable<System.DateTime> InicioPractica { get; set; }
        public Nullable<System.DateTime> FinPractica { get; set; }
        public Nullable<int> CarreraId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfertaHorario> OfertaHorario { get; set; }
        public virtual Carrera Carrera1 { get; set; }
    }
}
