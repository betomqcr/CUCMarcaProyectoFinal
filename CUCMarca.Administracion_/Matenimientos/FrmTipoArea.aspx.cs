using CUCMarca.BusinessServices;
using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmTipoArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarTiposArea();
            }
        }

        private void CargarTiposArea()
        {
            try
            {
                AreaService service = new AreaService();
                List<TipoArea> listaTipoArea = service.ObtenerTiposArea();
                gvMain.DataSource = listaTipoArea;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de área: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarTiposArea();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarTiposArea();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int areaid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                AreaService service = new AreaService();
                int result = service.BorrarTipoArea(areaid);
                if (result > 0)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error actualizando el tipo de área";
                }
                CargarTiposArea();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el tipo de área: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarTiposArea();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                int tipoAreaId = int.Parse(gvMain.Rows[rowid].Cells[0].Text);//int.Parse(e.NewValues["AreaID"].ToString());
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del tipo de area.";
                }
                else
                {
                    AreaService service = new AreaService();
                    int result = service.ActualizarTipoArea(tipoAreaId, nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando los tipos de área.";
                    }
                }
                gvMain.EditIndex = -1;
                CargarTiposArea();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los tipos de área: " + exc.Message;
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
                    FailureText.Text = "Debe indicar el nombre del tipo de area.";
                }
                else
                {
                    AreaService service = new AreaService();
                    int result = service.NuevoTipoArea(nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el tipo de área";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarTiposArea();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el tipo de área: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
        }
    }
}