using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace CUCMarca.WebSite.Models
{
    public class Clave
    {
        [DisplayName("Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string Contrasena { get; set; }

        [DisplayName("Nueva Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string newContrasena { get; set; }


        [DisplayName("Confrimar Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string ConfirmContrasena { get; set; }


        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public override string ToString()
        {
            return ToJsonString();
        }
    }
}