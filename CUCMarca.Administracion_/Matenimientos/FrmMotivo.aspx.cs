using CUCMarca.BusinessServices;
using CUCMarca.DataAccess;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CUCMarca.Controllers;


namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmMotivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarMotivo();

                
            }
        }

        private void CargarMotivo()
        {
            try
            {

                MotivoesController ms = new MotivoesController();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Nombre");

                foreach (CUCMarca.Controllers.respuesta b in ms.GetMotivo().ToList())
                {
                    dt.Rows.Add(new Object[] { b.MotivoID, b.nombre});
                }
                gvMain.DataSource = dt;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de motivo: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarMotivo();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarMotivo();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int motivoid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);

                CUCMarcaEntities bd = new CUCMarcaEntities();
                Motivo m = bd.Motivo.FirstOrDefault(x => x.MotivoID == motivoid);

                int contM = bd.Justificacion.Where(x => x.MotivoID == motivoid).ToList().Count;

                if (contM == 0)
                {

                    bd.Motivo.Remove(m);

                    int result = bd.SaveChanges();

                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos eliminados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error eliminando el motivo";
                    }
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error eliminando el motivo. Esta siendo utilizado";
                }
                CargarMotivo();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el motivo: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarMotivo();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                int motivoId = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del motivo.";
                }
                else
                {
                    CUCMarcaEntities bd = new CUCMarcaEntities();
                    Motivo m = bd.Motivo.FirstOrDefault(x => x.MotivoID == motivoId);

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
                        FailureText.Text = "Ha ocurrido un error actualizando los motivo.";
                    }
                }
                gvMain.EditIndex = -1;
                CargarMotivo();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los motivo: " + exc.Message;
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
                    FailureText.Text = "Debe indicar el nombre del motivo.";
                }
                else
                {
                    CUCMarcaEntities bd = new CUCMarcaEntities();

                    Motivo m = new Motivo();

                    m.Nombre = nombre;
                    bd.Motivo.Add(m);
                   
                    int result = bd.SaveChanges();
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el tipo de motivo";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarMotivo();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el tipo de motivo: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
        }
    }
}