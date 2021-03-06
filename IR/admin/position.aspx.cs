﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR.admin
{
    public partial class position : System.Web.UI.Page
    {
        UserAccountDataContext dbUser = new UserAccountDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvPositions.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvPositions.Rows.Count > 0)
            {
                string strSearch = txtSearch.Text;
                var q = (from cc in dbUser.Positions
                         where
                         (
                             cc.Name.Contains(strSearch)
                         )
                         select new
                         {
                             Name = cc.Name
                         }).ToList();

                GridView1.DataSource = q;
                GridView1.DataBind();
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Positions");
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
                    Response.AddHeader("content-disposition", "attachment;  filename=Positions.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void gvPositions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int Id = Convert.ToInt32(gvPositions.DataKeys[index].Value.ToString());

                //load
                var q = (from cc in dbUser.Positions
                         where cc.Id == Id
                         select cc).FirstOrDefault();

                hfEditId.Value = q.Id.ToString();
                txtEditPosition.Text = q.Name;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int Id = Convert.ToInt32(gvPositions.DataKeys[index].Value.ToString());

                hfDeleteId.Value = Id.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Position pos = new Position();
            pos.Name = txtAddPosition.Text;
            dbUser.Positions.InsertOnSubmit(pos);

            dbUser.SubmitChanges();
            this.gvPositions.DataBind();

            //audit trail
            DBLogger.Log("Create", "Created Position ", pos.Name);

            //hide modal
            Javascript.HideModal(this, this, "addModal");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfEditId.Value);
            var pos = (from p in dbUser.Positions where p.Id == Id select p).FirstOrDefault();
            pos.Name = txtEditPosition.Text;

            dbUser.SubmitChanges();
            this.gvPositions.DataBind();

            //audit trail
            DBLogger.Log("Update", "Updated Position ", pos.Name);

            //hide modal
            Javascript.HideModal(this, this, "updateModal");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfDeleteId.Value);
            var pos = (from p in dbUser.Positions where p.Id == Id select p).FirstOrDefault();

            dbUser.Positions.DeleteOnSubmit(pos);

            dbUser.SubmitChanges();
            this.gvPositions.DataBind();

            //audit trail
            DBLogger.Log("Delete", "Deleted Position ", pos.Name);

            //hide modal
            Javascript.HideModal(this, this, "deleteModal");
        }

        protected void PositionsDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var pos = (from p in dbUser.Positions
                       where p.Name.Contains(strSearch)
                       select p).ToList();

            txtSearch.Focus();
            e.Result = pos;
        }

        protected void gvPositions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //dont delete 'None' position
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                Label posId = (Label)e.Row.FindControl("lblRowId");

                if(posId.Text == "1")
                {
                    btnDelete.Visible = false;
                }
            }
        }
    }
}