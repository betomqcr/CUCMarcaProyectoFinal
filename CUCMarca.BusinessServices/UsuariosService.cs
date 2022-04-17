using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public class UsuariosService
    {

        private CUCMarcaEntities entities;

        public UsuariosService()
        {
            entities = new CUCMarcaEntities();
        }

        public List<AspNetUsers> ObtenerUsuarios()
        {
            List<AspNetUsers> users = entities.AspNetUsers.ToList<AspNetUsers>();
            return users;
        }

    }
}
