using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Globalization;

namespace CUCMarca.WebSite.Models
{
    public partial class Asistencia
    {
        [DisplayName("Identificación")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Debe indicar una identificación válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string CodigoFuncionario { get; set; }
        public string TipoMarca { get; set; }

        [DisplayName("Actividad")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Debe indicar una actividad")]
        public string Actividad { get; set; }

        public string idFuncionario { get; set; }


        [DisplayName("Comentarios")]
        [StringLength(8000, MinimumLength = 0, ErrorMessage = "Debe indicar un comentario")]
        public string Comentarios { get; set; }

        [DisplayName("Contraseña")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Debe indicar una contraseña válida")]
        [Required(ErrorMessage = "El campo Identificación es requerido")]
        public string Contrasena { get; set; }

        public string DireccionIP
        { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public decimal _Latitud { get; set; }

        public decimal _Longitud { get; set; }

        // string

        public decimal GetDecimal(string s)
        {
            decimal d;

            var formatinfo = new NumberFormatInfo();

            formatinfo.NumberDecimalSeparator = ".";

            if (decimal.TryParse(s, NumberStyles.Float, formatinfo, out d))
            {
                return d;
            }

            formatinfo.NumberDecimalSeparator = ",";

            if (decimal.TryParse(s, NumberStyles.Float, formatinfo, out d))
            {
                return d;
            }
            throw new SystemException(string.Format("strange number format '{0}'", s));
        }
    

        public IEnumerable<SelectListItem> Values
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "E", Text = "Entrada" },
                new SelectListItem { Value = "S", Text = "Salida" }
            };
            }
        }
        public IEnumerable<SelectListItem> Lista
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "0", Text = "Seleccione una actividad" }

                };
            }
        }

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