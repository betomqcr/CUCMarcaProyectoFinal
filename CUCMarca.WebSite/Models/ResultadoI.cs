using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUCMarca.WebSite.Models
{
    public class ResultadoI
    {
        public int InconsistenciaID { get; set; }
        public int HorarioID { get; set; }
        public string CodigoFuncionario { get; set; }
        public System.DateTime FechaInconsistencia { get; set; }
        public byte Estado { get; set; }
        public int TipoInconsistenciaID { get; set; }
        public string TipoInconsistencia { get; set; }
        public int AreaID { get; set; }
        public string NombreArea { get; set; }
        public string identificacion { get; set; }


    }
    
}