
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
    
public partial class Funcionario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Funcionario()
    {

        this.FuncionarioArea = new HashSet<FuncionarioArea>();

        this.Area = new HashSet<Area>();

        this.Excepcion = new HashSet<Excepcion>();

    }


    public int FuncionarioID { get; set; }

    public int TipoIdentificacionID { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public int TipoFuncionarioID { get; set; }

    public string Correo { get; set; }

    public string Contrasena { get; set; }

    public string Identificacion { get; set; }

    public Nullable<byte> Estado { get; set; }



    public virtual TipoFuncionario TipoFuncionario { get; set; }

    public virtual TipoIdentificacion TipoIdentificacion { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<FuncionarioArea> FuncionarioArea { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Area> Area { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Excepcion> Excepcion { get; set; }

}

}
