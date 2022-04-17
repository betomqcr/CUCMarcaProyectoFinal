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
    public partial class Exepcion
    {
        public DateTime FechaExcepcion { get; set; }
        public string Observaciones { get; set; }
        public int MotivoID { get; set; }
        public bool ReponeTiempo { get; set; }
        public DateTime FechaReposicion { get; set; }

        public string TipoRepone { get; set; }


        public IEnumerable<SelectListItem> Values
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "false", Text = "NO" },
                new SelectListItem { Value = "true", Text = "SI" }
            };
            }
        }
        public IEnumerable<SelectListItem> Lista
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "0", Text = "Seleccione un Motivo" }

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