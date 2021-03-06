
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace CUCMarca.DataAccess
{

using System;
    using System.Collections.Generic;
    
public partial class Inconsistencia
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Inconsistencia()
    {

        this.Justificacion = new HashSet<Justificacion>();

    }


    public int InconsistenciaID { get; set; }

    public int HorarioID { get; set; }

    public string CodigoFuncionario { get; set; }

    public System.DateTime FechaInconsistencia { get; set; }

    public byte Estado { get; set; }

    public bool Notificar { get; set; }

    public int TipoInconsistenciaID { get; set; }

    public string RegistradoPor { get; set; }



    public virtual FuncionarioArea FuncionarioArea { get; set; }

    public virtual Horario Horario { get; set; }

    public virtual TipoInconsistencia TipoInconsistencia { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Justificacion> Justificacion { get; set; }

}

}
