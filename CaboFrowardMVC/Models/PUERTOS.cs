//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaboFrowardMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PUERTOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PUERTOS()
        {
            this.LOCACIONES = new HashSet<LOCACIONES>();
            this.SOLICITUDES = new HashSet<SOLICITUDES>();
        }
    
        public int ID_PUERTO { get; set; }
        public string NOMBRE { get; set; }
        public string rut { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCACIONES> LOCACIONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLICITUDES> SOLICITUDES { get; set; }
    }
}
