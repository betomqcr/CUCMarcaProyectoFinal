//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CUCMarca.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asistencia
    {
        public int AsistenciaID { get; set; }
        public int FuncionarioID { get; set; }
        public System.DateTime FechaAsistencia { get; set; }
        public string TipoMarca { get; set; }
        public string Actividad { get; set; }
        public string Comentarios { get; set; }
    
        public virtual Funcionario Funcionario { get; set; }
    }
}
