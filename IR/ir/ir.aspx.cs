using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
                this.gvIR.DataBind();
                txtSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvIR.DataBind();
            txtSearch.Focus();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if(gvIR.Rows.Count > 0)
            {
                string strSearch = txtSearch.Text;

                var q = (from ir in db.IRTransactions
                         join cc in db.CrisisCodes
                         on ir.CrisisId equals cc.Id
                         where
                         (
                            ir.TicketNo.Contains(strSearch) ||
                            cc.Code.Contains(strSearch) ||
                            cc.Name.Contains(strSearch) ||
                            ir.Subject.Contains(strSearch) ||
                            ir.Room.Contains(strSearch) ||
                            ir.Status.Contains(strSearch)
                         )
                         select new
                         {
                             TicketNo = ir.TicketNo,
                             CrisisName = cc.Name,
                             Subject = ir.Subject,
                             Room = ir.Room,
                             IncidentDate = ir.IncidentDate,
                             Status = ir.Status
                         }).ToList();

                GridView1.DataSource = q;
                GridView1.DataBind();
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Incident Report");
                var totalCols = GridView1.Rows[0].Cells.Count;
                var totalRows = GridView1.Rows.Count;
                var headerRow = GridView1.HeaderRow;
                for (var i = 1; i <= totalCols; i++)
                {
                    workSheet.Cells[1, i].Value = headerRow.Cells[i - 1].Text;
                }
                for (var j = 1; j <= totalRows; j++)
                {
                    for (var i = 1; i <= totalCols; i++)
                    {
                        var product = q.ElementAt(j - 1);
                        workSheet.Cells[j + 1, i].Value = product.GetType().GetProperty(headerRow.Cells[i - 1].Text).GetValue(product, null);
                    }
                }
                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=Incident-Report.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void gvIR_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void IRDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var q = (from ir in db.IRTransactions
                     join cc in db.CrisisCodes
                     on ir.CrisisId equals cc.Id
                     where
                     (
                        ir.TicketNo.Contains(strSearch) ||
                        cc.Code.Contains(strSearch) ||
                        cc.Name.Contains(strSearch) ||
                        ir.Subject.Contains(strSearch) ||
                        ir.Room.Contains(strSearch) ||
                        ir.Status.Contains(strSearch)
                     )
                     select new
                     {
                         TicketNo = ir.TicketNo,
                         CrisisName = cc.Name,
                         Subject = ir.Subject,
                         Room = ir.Room,
                         IncidentDate = ir.IncidentDate,
                         Status = ir.Status
                     }).ToList();

            e.Result = q;
        }
    }
}