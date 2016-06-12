﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IR.ir
{
    public partial class ir : System.Web.UI.Page
    {
        IRContextDataContext dbIR = new IRContextDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();
        UserAccountDataContext dbUser = new UserAccountDataContext();


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

                var q = (from ir in dbIR.IRTransactions
                         join cc in dbIR.CrisisCodes
                         on ir.CrisisId equals cc.Id
                         where
                         (
                            ir.TicketNo.Contains(strSearch) ||
                            cc.Code.Contains(strSearch) ||
                            cc.Name.Contains(strSearch) ||
                            ir.Subject.Contains(strSearch) ||
                            ir.Room.Contains(strSearch)
                         )
                         select new
                         {
                             IR = ir.TicketNo,
                             CrisisName = cc.Name,
                             Subject = ir.Subject,
                             Room = ir.Room,
                             IncidentDate = ir.WhenIncidentHappen,
                             Status = ir.Status,
                             DateSolved = ir.DateSolved,
                             From = ir.From,
                             PreparedBy = ir.PreparedBy
                         }).ToList();


                if (txtFromDate.Text != String.Empty && txtToDate.Text == String.Empty)
                {
                    q = q.Where(x => x.IncidentDate.Value.Date >= Convert.ToDateTime(txtFromDate.Text).Date).ToList();
                }

                if (txtToDate.Text != String.Empty && txtFromDate.Text == String.Empty)
                {
                    q = q.Where(x => x.IncidentDate.Value.Date <= Convert.ToDateTime(txtToDate.Text).Date).ToList();
                }

                if (txtFromDate.Text != String.Empty && txtToDate.Text != String.Empty)
                {
                    q = q.Where(x => (x.IncidentDate.Value.Date >= Convert.ToDateTime(txtFromDate.Text).Date &&
                        x.IncidentDate.Value.Date <= Convert.ToDateTime(txtToDate.Text).Date)).ToList();
                }

                if (ddlStatus.SelectedValue != "0")
                {
                    q = q.Where(x => x.Status == ddlStatus.SelectedValue).ToList();
                }

                if (User.IsInRole("can-create-ir"))
                {
                    q = q.Where(x => x.PreparedBy == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())).ToList();
                }

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
            else if (e.CommandName.Equals("solveRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int irId = Convert.ToInt32(gvIR.DataKeys[index].Value);

                hfSolveId.Value = irId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#solveModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = (from ir in dbIR.IRTransactions
                     where ir.Id == Convert.ToInt32(hfDeleteId.Value)
                     select ir).FirstOrDefault();

            dbIR.IRTransactions.DeleteOnSubmit(q);
            dbIR.SubmitChanges();

            this.gvIR.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void IRDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var q = (from ir in dbIR.IRTransactions
                     join cc in dbIR.CrisisCodes
                     on ir.CrisisId equals cc.Id
                     where
                     (
                        ir.TicketNo.Contains(strSearch) ||
                        cc.Code.Contains(strSearch) ||
                        cc.Name.Contains(strSearch) ||
                        ir.Subject.Contains(strSearch) ||
                        ir.Room.Contains(strSearch)
                     )
                     select new
                     {
                         Id = ir.Id,
                         TicketNo = ir.TicketNo,
                         CrisisName = cc.Name,
                         Subject = ir.Subject,
                         Room = ir.Room,
                         IncidentDate = ir.WhenIncidentHappen,
                         Status = ir.Status,
                         DateSolved = ir.DateSolved,
                         PreparedBy = ir.PreparedBy,
                         RenderedTime = String.Format("{0} hours, {1} min", DateTime.Now.Subtract(ir.StartDate.Value).Hours, DateTime.Now.Subtract(ir.StartDate.Value).Minutes)
                     }).ToList();

            if(txtFromDate.Text != String.Empty && txtToDate.Text == String.Empty)
            {
                q = q.Where(x => x.IncidentDate.Value.Date >= Convert.ToDateTime(txtFromDate.Text).Date).ToList();
            }
            
            if(txtToDate.Text != String.Empty && txtFromDate.Text == String.Empty)
            {
                q = q.Where(x => x.IncidentDate.Value.Date <= Convert.ToDateTime(txtToDate.Text).Date).ToList();
            }

            if(txtFromDate.Text != String.Empty && txtToDate.Text != String.Empty)
            {
                q = q.Where(x => (x.IncidentDate.Value.Date >= Convert.ToDateTime(txtFromDate.Text).Date && 
                    x.IncidentDate.Value.Date <= Convert.ToDateTime(txtToDate.Text).Date)).ToList();
            }

            if(ddlStatus.SelectedValue != "0")
            {
                q = q.Where(x => x.Status == ddlStatus.SelectedValue).ToList();
            }

            if(User.IsInRole("can-create-ir"))
            {
                q = q.Where(x => x.PreparedBy == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())).ToList();
            }

            e.Result = q;
            txtSearch.Focus();
        }

        protected void btnConfirmSolved_Click(object sender, EventArgs e)
        {
            int irId = Convert.ToInt32(hfSolveId.Value);
            var q = (from ir in dbIR.IRTransactions
                     where ir.Id == irId
                     select ir).FirstOrDefault();

            q.Status = "Solved";
            q.DateSolved = Convert.ToDateTime(txtSolvedDate.Text);
            dbIR.SubmitChanges();

            this.gvIR.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#solveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        //protected void gvIR_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if(e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblStatus = e.Row.FindControl("lblStatus") as Label;
        //        Button btnSolve = e.Row.FindControl("btnSolved") as Button;

        //        if(lblStatus.Text == "Solved")
        //        {
        //            btnSolve.Visible = false;
        //        }
        //    }
        //}

        protected void btnExportTotalbyIR_Click(object sender, EventArgs e)
        {
            if (gvIR.Rows.Count > 0)
            {
                var q = (from ir in dbIR.IRTransactions
                         join cc in dbIR.CrisisCodes
                         on ir.CrisisId equals cc.Id
                         group ir by ir.CrisisId into irg
                         select new
                         {
                             IRCode = (from ccc in dbIR.CrisisCodes
                                       where ccc.Id == irg.Key
                                      select new {
                                          IR = ccc.Name
                                      }).FirstOrDefault().IR,
                             Total = irg.Count()
                         }).ToList();


                GridView2.DataSource = q;
                GridView2.DataBind();
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Incident Report");
                var totalCols = GridView2.Rows[0].Cells.Count;
                var totalRows = GridView2.Rows.Count;
                var headerRow = GridView2.HeaderRow;
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
    }
}