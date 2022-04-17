using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUCMarca.Models
{
    public class Marca
    {
        public long AsistenciaID { get; set; }
        public DateTime FechaMarca { get; set; }
        public int FuncionarioId { get; set; }
        public string Mensaje { get; set; }
        public string Actividad { get; set; }
    }
}