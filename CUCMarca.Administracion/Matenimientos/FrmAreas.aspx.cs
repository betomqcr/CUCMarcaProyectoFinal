using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CUCMarca.BusinessServices;

using CUCMarca.DataAccess;
using Microsoft.Ajax.Utilities;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmAreas : System.Web.UI.Page
    {

        public List<CUCMarca.DataAccess.TipoArea> listaTipoArea;
        public List<Resultado> listaJefeArea;

        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            listaJefeArea = new List<Resultado>();
            IniciarAreas();
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarAreas();
                CargarTiposArea();
                CargarJefesArea();
            }

        }

        private void IniciarAreas()
        {
            AreaService service = new AreaService();
            listaTipoArea = service.ObtenerTiposArea();
            listaJefeArea = service.ObtenerJefes();
        }

        public string ObtenerNombreTipoArea(int tipoID)
        { 
            IEnumerable<string> nombre = from l in listaTipoArea
                                         where l.TipoAreaID == tipoID
                                         select l.Nombre;
            return nombre.FirstOrDefault();
        }
        public string ObtenerNombreJefeArea(int idfunci)
        {
            IEnumerable<string> nombre = from l in listaJefeArea
                                         where l.codigofuncionario == idfunci
                                         select l.nombrecompleto;
            return nombre.FirstOrDefault();
        }

        private void CargarJefesArea()
        {
            try
            {
                listaJefeArea.Insert(0, new Resultado()
                {
                    codigofuncionario = 0,
                    nombrecompleto = "Seleccione el jefe de área"
                });
                ddlJefeArea.DataSource = listaJefeArea;
                ddlJefeArea.DataTextField = "nombrecompleto";
                ddlJefeArea.DataValueField = "codigofuncionario";
                ddlJefeArea.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas: " + exc.Message;
            }
        }


        private void CargarTiposArea()
        {
            try
            {
                listaTipoArea.Insert(0, new TipoArea()
                {
                    TipoAreaID = 0,
                    Nombre = "Seleccione el tipo de área"
                });
                ddlTipoArea.DataSource = listaTipoArea;
                ddlTipoArea.DataTextField = "Nombre";
                ddlTipoArea.DataValueField = "TipoAreaID";
                ddlTipoArea.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas: " + exc.Message;
            }
        }

        private void CargarAreas()
        {
            try
            {
                AreaService service = new AreaService();
                gvMain.DataSource = service.ObtenerAreas();
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                int tipoAreaId = int.Parse(ddlTipoArea.SelectedValue);
                int jefeAreaid = int.Parse(ddlJefeArea.SelectedValue);
                if (string.IsNullOrEmpty(nombre) || tipoAreaId < 1 || jefeAreaid < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre, el tipo de área y el jefe de área.";
                }
                else
                {
                    AreaService service = new AreaService();
                    int result = service.NuevaArea(nombre, tipoAreaId, jefeAreaid);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando las áreas";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarAreas();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando las áreas: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            ddlTipoArea.SelectedIndex = 0;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            mvMain.SetActiveView(viewDatos);
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropDown = e.Row.Cells[2].FindControl("ddlTipoAreaGrid") as DropDownList;
                DropDownList dropDown1 = e.Row.Cells[1].FindControl("ddljefeGrid") as DropDownList;
                if (dropDown != null)
                    dropDown.SelectedValue = DataBinder.Eval(e.Row.DataItem, "TipoAreaID").ToString();
                if (dropDown1 != null)
                    dropDown1.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Jefe").ToString();
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarAreas();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                DropDownList ddl = gvMain.Rows[rowid].FindControl("ddlTipoAreaGrid") as DropDownList;
                int tipoAreaId = int.Parse(ddl.SelectedValue);
                DropDownList ddl2 = gvMain.Rows[rowid].FindControl("ddljefeGrid") as DropDownList;
                int jefeAreaId = int.Parse(ddl2.SelectedValue);
                int areaId = int.Parse(gvMain.Rows[rowid].Cells[0].Text);//int.Parse(e.NewValues["AreaID"].ToString());

                if (string.IsNullOrEmpty(nombre) || tipoAreaId < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre y el tipo de area.";
                }
                else 
                {
                    AreaService service = new AreaService();
                    int result = service.ActualizarArea(areaId, nombre, tipoAreaId, jefeAreaId);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else 
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando las áreas";
                    }
                }
                gvMain.EditIndex = -1;
                CargarAreas();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando las áreas: " + exc.Message;
            }
        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int areaid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                AreaService service = new AreaService();
                int result = service.BorrarArea(areaid);
                if (result > 0)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error actualizando las áreas";
                }
                CargarAreas();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando las áreas" + exc.Message;
            }
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarAreas();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarAreas();
        }
    }
}