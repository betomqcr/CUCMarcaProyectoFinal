using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public class HorarioFuncionario
    {
        public int HorarioID
        { get; set; }

        public string CodigoFuncionario
        { get; set; }

        public int Periodo
        { get; set; }

        public int Anio
        { get; set; }

        public byte Estado
        { get; set; }

        public string NombreArea
        { get; set; }
    }
}
