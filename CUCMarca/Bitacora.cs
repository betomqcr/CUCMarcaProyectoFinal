using CUCMarca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;


namespace CUCMarca
{
    public class Bitacora
    {
        public DateTime fecha { get; set; }
        public string accion { get; set; }
        public string descripcion { get; set; }
        public string usuario { get; set; }
        public string tipo { get; set; }

        public void ins_bitacora()
        {

            if ((accion != "") && (descripcion != "") && (usuario != "") && (tipo != ""))
            {
                CUCMarcaEntities db = new CUCMarcaEntities();

                Models.Bitacora b = new  Models.Bitacora();

                b.Accion = accion;
                b.Descripcion = descripcion;
                b.Fecha = fecha;
                b.Usuario = usuario;
                b.Tipo = tipo;

                try
                {
                    db.Bitacora.Add(b);
                    db.SaveChanges();

                }
                catch (Exception ex) { }
            }
        }
    



    }
}