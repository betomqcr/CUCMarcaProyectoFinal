using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CUCMarca.WebSite.Models
{
    public class Marca
    {
        public long AsistenciaID { get; set; }
        public DateTime FechaMarca { get; set; }
        public int FuncionarioId { get; set; }
        [JsonIgnore]
        public int CodigoRespuesta { get; set; }
        public string Mensaje { get; set; }
        public string Actividad { get; set; }
    }
}