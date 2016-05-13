using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR.ir
{
    public partial class report_ir : System.Web.UI.Page
    {
        IRContextDataContext db = new IRContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);
                generateReport(Id);
            }
        }

        protected void generateReport(int Id)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ir/Report1.rdlc");
            ReportViewer1.SizeToReportContent = true;

            //load ir
            var q = (from ir in db.IRTransactions
                     where ir.Id == Id
                     select ir).FirstOrDefault(); ;

            ReportParameter[] param = new ReportParameter[5];

            param[0] = new ReportParameter("name", q.TicketNo);

            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}