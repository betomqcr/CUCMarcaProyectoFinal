using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public class DatosAsistencia
    {
        public long AsistenciaID { get; set; }
        public string CodigoFuncionario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public string TipoMarca { get; set; }
        public string DireccionIP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

    }
}
