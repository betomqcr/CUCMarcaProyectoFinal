using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CUCMarca
{

    public class C_Token
    {
        public C_Token()
        {

        }
        public string generarToken()
        {

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public bool validartoken(string token)
        {
            int tiempo = 0;
            tiempo = int.Parse(ConfigurationManager.AppSettings["timelimit"]);
            bool res = false;

            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(tiempo * -1))
            {
                res = false;

            }
            else
            {
                res = true;
            }

            return res;
        }
    }
}