using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CUCMarca.DataAccess;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmInconsistencias : System.Web.UI.Page
    {
        CUCMarcaEntities bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            bd = new CUCMarcaEntities();
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;

            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                cargarInconsistencias();

            }

        }
        public void cargarInconsistencias()
        {
            try
            {



                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Nombre");

                foreach (RespuestaTI b in getInconsistencias())
                {
                    dt.Rows.Add(new Object[] { b.TipoInconsistenciaID, b.Nombre });
                }
                gvMain.DataSource = dt;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de inconsistencias: " + exc.Message;
            }
        }
        public class RespuestaTI
        {
            public int TipoInconsistenciaID { get; set; }
            public string Nombre { get; set; }
        }

        public List<RespuestaTI> getInconsistencias()
        {

            List<RespuestaTI> lista = new List<RespuestaTI>();

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/TipoInconsistencias";


            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url);
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    lista = null;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    lista = JsonConvert.DeserializeObject<List<RespuestaTI>>(task2.Result);

                }
                else
                {
                    lista = null;
                }
            }
            return lista;
        }




        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            cargarInconsistencias();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            cargarInconsistencias();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int incoid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);

                CUCMarcaEntities bd = new CUCMarcaEntities();
                TipoInconsistencia m = bd.TipoInconsistencia.FirstOrDefault(x => x.TipoInconsistenciaID == incoid);

                int contI = bd.Inconsistencia.Where(x => x.TipoInconsistenciaID == incoid).ToList().Count;

                if (contI == 0)
                {
                    bd.TipoInconsistencia.Remove(m);

                    int result = bd.SaveChanges();

                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos eliminados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error eliminando el tipo de inconsistencia";
                    }
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error eliminando el tipo de inconsistencia. Esta siendo utilizado.";
                }


                cargarInconsistencias();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el tipo de inconsistencia: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            cargarInconsistencias();

        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                int incoid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del tipo de inconsistencia.";
                }
                else
                {
                    CUCMarcaEntities bd = new CUCMarcaEntities();
                    TipoInconsistencia m = bd.TipoInconsistencia.FirstOrDefault(x => x.TipoInconsistenciaID == incoid);

                    m.Nombre = nombre;

                    int result = bd.SaveChanges();

                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando los tipos de inconsistencias.";
                    }
                }
                gvMain.EditIndex = -1;
                cargarInconsistencias();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los tipos de inconsistencias: " + exc.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            mvMain.SetActiveView(viewDatos);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del tipo de inconsistencia.";
                }
                else
                {
                    CUCMarcaEntities bd = new CUCMarcaEntities();

                    TipoInconsistencia m = new TipoInconsistencia();

                    m.Nombre = nombre;
                    bd.TipoInconsistencia.Add(m);

                    int result = bd.SaveChanges();
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el tipo de inconsistencia";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                cargarInconsistencias();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el tipo de inconsistencia: " + exc.Message;
            }
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
        }
    }
}