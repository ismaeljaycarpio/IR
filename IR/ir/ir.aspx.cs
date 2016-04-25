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
        EHRISDataContext dbEHRIS = new EHRISDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvIR.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvIR.Rows.Count > 0)
            {
                string strSearch = txtSearch.Text;

                var employee = (from emp in dbEHRIS.EMPLOYEEs
                                select emp).ToList();

                var q = (from empl in employee
                         join ir in db.IRTransactions
                         on empl.UserId equals ir.From
                         join cc in db.CrisisCodes
                         on ir.CrisisId equals cc.Id
                         where
                         (
                            ir.TicketNo.Contains(strSearch) ||
                            cc.Code.Contains(strSearch) ||
                            cc.Name.Contains(strSearch) ||
                            ir.Subject.Contains(strSearch) ||
                            ir.Room.Contains(strSearch) ||
                            empl.LastName.Contains(strSearch) ||
                            empl.FirstName.Contains(strSearch) ||
                            empl.MiddleName.Contains(strSearch) ||
                            ir.Status.Contains(strSearch)
                         )
                         select new
                         {
                             TicketNo = ir.TicketNo,
                             CrisisName = cc.Name,
                             Subject = ir.Subject,
                             Room = ir.Room,
                             IncidentDate = ir.WhenIncidentHappen,
                             Status = ir.Status,
                             From = empl.LastName + " , " + empl.FirstName + " " + empl.MiddleName
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
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int irId = Convert.ToInt32(gvIR.DataKeys[index].Value);

                Response.Redirect("~/ir/view-irform.aspx?Id=" + irId.ToString());
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int irId = Convert.ToInt32(gvIR.DataKeys[index].Value);

                hfDeleteId.Value = irId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
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
                         Id = ir.Id,
                         TicketNo = ir.TicketNo,
                         CrisisName = cc.Name,
                         Subject = ir.Subject,
                         Room = ir.Room,
                         IncidentDate = ir.WhenIncidentHappen,
                         Status = ir.Status
                     }).ToList();

            e.Result = q;
            txtSearch.Focus();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from ir in db.IRTransactions
                     where ir.Id == Convert.ToInt32(hfDeleteId.Value)
                     select ir).FirstOrDefault();

            db.IRTransactions.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvIR.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }
    }
}