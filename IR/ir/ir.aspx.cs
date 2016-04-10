using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR.ir
{
    public partial class ir : System.Web.UI.Page
    {
        IRContextDataContext db = new IRContextDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void gvIR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvIR_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void bindGridview()
        {
            var q = (from ir in db.IRTransactions
                     select ir).ToList();
            gvIR.DataSource = q;
            gvIR.DataBind();
        }
    }
}