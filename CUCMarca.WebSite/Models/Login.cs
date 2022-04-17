using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CUCMarca.WebSite.Models
{
    public class Login
    {
        [DisplayName("Identificación")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Debe indicar una identificación válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string identificacion { get; set; }

        [DisplayName("Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public string Contraseña { get; set; }

        public int Modulo { get; set; }
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