
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace CUCMarca.Models
{

using System;
    using System.Collections.Generic;
    
public partial class TipoInconsistencia
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TipoInconsistencia()
    {

        this.Inconsistencia = new HashSet<Inconsistencia>();

    }


    public int TipoInconsistenciaID { get; set; }

    public string Nombre { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Inconsistencia> Inconsistencia { get; set; }

}

}