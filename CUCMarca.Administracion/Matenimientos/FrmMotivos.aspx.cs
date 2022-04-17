using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CUCMarca;
using CUCMarca.DataAccess;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmMotivos : System.Web.UI.Page
    {
        CUCMarcaEntities bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            bd = new CUCMarcaEntities();
            gvMain.DataSource = bd.Motivo;
            gvMain.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}