using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CUCMarca.Models
{
    public class AsistenciaObj
    {
        [DisplayName("Identificación")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Debe indicar un código de funcionario válido")]
        [Required(ErrorMessage = "El campo código de funcionario es requerido")]
        public string CodigoFuncionario { get; set; }

        public int AreaID { get; set; }
        public string idFuncionario { get; set; }
        public string TipoMarca { get; set; }

        [DisplayName("Actividad")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Debe indicar una actividad")]
        public string Actividad { get; set; }

        [DisplayName("Comentarios")]
        [StringLength(8000, MinimumLength = 0, ErrorMessage = "Debe indicar un comentario")]
        public string Comentarios { get; set; }

        [DisplayName("Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string Contrasena { get; set; }

        public string DireccionIP
        { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

    }
}