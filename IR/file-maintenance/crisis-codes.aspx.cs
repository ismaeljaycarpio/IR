using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR.file_maintenance
{
    public partial class crisis_codes : System.Web.UI.Page
    {
        IRContextDataContext db = new IRContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.gvCrisisCodes.DataBind();
                txtSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvCrisisCodes.DataBind();
            txtSearch.Focus();
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void gvCrisisCodes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int Id = Convert.ToInt32(gvCrisisCodes.DataKeys[index].Value.ToString());

                //load
                var q = (from cc in db.CrisisCodes
                         where cc.Id == Id
                         select cc).FirstOrDefault();

                hfEditId.Value = q.Id.ToString();
                txtEditCrisisCode.Text = q.Code;
                txtEditCrisisName.Text = q.Name;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int Id = Convert.ToInt32(gvCrisisCodes.DataKeys[index].Value.ToString());

                hfDeleteId.Value = Id.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CrisisCode cc = new CrisisCode();
            cc.Code = txtAddCrisisCode.Text;
            cc.Name = txtAddCrisisName.Text;
            db.CrisisCodes.InsertOnSubmit(cc);
            db.SubmitChanges();

            this.gvCrisisCodes.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfEditId.Value);
            var q = (from cc in db.CrisisCodes
                     where cc.Id == Id
                     select cc).FirstOrDefault();

            q.Code = txtEditCrisisCode.Text;
            q.Name = txtEditCrisisName.Text;
            db.SubmitChanges();

            this.gvCrisisCodes.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(hfDeleteId.Value);
            var q = (from cc in db.CrisisCodes
                     where cc.Id == Id
                     select cc).FirstOrDefault();

            db.CrisisCodes.DeleteOnSubmit(q);
            db.SubmitChanges();

            this.gvCrisisCodes.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void CrisisCodesDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var q = (from cc in db.CrisisCodes
                     where
                     (
                         cc.Code.Contains(strSearch) ||
                         cc.Name.Contains(strSearch)
                     )
                     select cc).ToList();

            e.Result = q;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if(gvCrisisCodes.Rows.Count > 0)
            {
                string strSearch = txtSearch.Text;
                var q = (from cc in db.CrisisCodes
                         where
                         (
                             cc.Code.Contains(strSearch) ||
                             cc.Name.Contains(strSearch)
                         )
                         select new
                         {
                             Code = cc.Code,
                             Name = cc.Name
                         }).ToList();

                GridView1.DataSource = q;
                GridView1.DataBind();
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Crisis Codes");
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
                    Response.AddHeader("content-disposition", "attachment;  filename=Crisis-Codes.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}