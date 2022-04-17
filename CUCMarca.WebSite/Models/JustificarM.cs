using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CUCMarca.WebSite.Models
{
    public class JustificarM
    {
        [Required(ErrorMessage = "Ingrese una fecha valida.")]
        [DataType(DataType.Date)]
        public DateTime FechaReposicion { get; set; }

        [DisplayName("Observaciones")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Debe indicar una observación válida")]
        [Required(ErrorMessage = "El campo Observación es requerido")]
        public string Observacion { get; set; }

        public int CodigoInconsistencia { get; set; }

        public bool ReponeTiempo { get; set; }

        public int idMotivo { get; set; }
        public int CodigoFuncionario { get; set; }

        public IEnumerable<SelectListItem> Motivo
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "0", Text = "Seleccione el motivo" }

                };
            }
        }

        public IEnumerable<SelectListItem> Inconsistencia
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "0", Text = "Seleccione el motivo" }

                };
            }
        }

        public IEnumerable<SelectListItem> reponet
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "True", Text = "Si" },
                new SelectListItem { Value = "False", Text = "No" }
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

    public class AprobarM
    {
        public string identificación { get; set; }

        public int JustificacionID { get; set; }
        public string NombreFuncionario { get; set; }
        public DateTime FechaInconsistencia { get; set; }

        public DateTime FechaJustificacion { get; set; }
        public DateTime FechaReposicion { get; set; }

        public string Observaciones { get; set; }

        public int CodigoInconsistencia { get; set; }
        public int InconsistenciaID { get; set; }
        public bool ReponeTiempo { get; set; }

        public int idMotivo { get; set; }

        public string Motivo { get; set; }

        public int CodigoFuncionario { get; set; }

        public int Autorizadopor { get; set; }

        public DateTime FechaAutorizacion { get; set; }

        public string tipo { get; set; }
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