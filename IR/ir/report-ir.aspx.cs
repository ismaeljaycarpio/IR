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
        UserAccountDataContext dbUser = new UserAccountDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Request.QueryString["Id"] == null)
                {
                    Response.Redirect("~/ir/ir.aspx");
                }

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
            var tran = (from ir in db.IRTransactions
                     join cc in db.CrisisCodes
                     on ir.CrisisId equals cc.Id
                     where ir.Id == Id
                     select new
                     {
                         Id = ir.Id,
                         TicketNo = ir.TicketNo,
                         CrisisName = cc.Name,
                         From = ir.From,
                         Date = ir.Date,
                         //Department = ir.DepartmentId,
                         Status = ir.Status,
                         Subject = ir.Subject,
                         Location = ir.Room,
                         PartiesInvolved = ir.WhoInvolved,
                         WhatHappened = ir.WhatHappened,
                         Investigation = ir.Investigation,
                         ActionTaken = ir.ActionTaken,
                         Recommendation = ir.Recommendation,
                         PreparedBy = ir.PreparedBy
                     }).FirstOrDefault(); ;

            string from = String.Empty;
            string date = String.Empty;
            string department = String.Empty;
            string preparedBy = String.Empty;

            if(tran.From != null)
            {
                var fromUser = (from up in dbUser.UserProfiles
                           where up.UserId == tran.From
                           select new
                           {
                               FullName = up.FirstName + " " + up.MiddleName + " " + up.LastName
                           }).FirstOrDefault();

                if(fromUser != null)
                {
                    from = fromUser.FullName;
                }    
            }

            if(tran.Date != null)
            {
                date = tran.Date.Value.ToShortDateString();
            }

            //if(tran.Department != null)
            //{
            //    var dept = (from d in dbEHRIS.DEPARTMENTs
            //                where d.Id == tran.Department
            //                select new
            //                {
            //                    Department = d.Department1
            //                }).FirstOrDefault();

            //    if(dept != null)
            //    {
            //        department = dept.Department;
            //    }
            //}

            ReportParameter[] param = new ReportParameter[14];
            param[0] = new ReportParameter("TicketNo", tran.TicketNo);
            param[1] = new ReportParameter("CrisisName", tran.CrisisName);
            param[2] = new ReportParameter("From", from);
            param[3] = new ReportParameter("Date", date);
            param[4] = new ReportParameter("Department", department);
            param[5] = new ReportParameter("Status", tran.Status);
            param[6] = new ReportParameter("Subject", tran.Subject);
            param[7] = new ReportParameter("Location", tran.Location);
            param[8] = new ReportParameter("PartiesInvolved", tran.PartiesInvolved);
            param[9] = new ReportParameter("WhatHappened", tran.WhatHappened);
            param[10] = new ReportParameter("Investigation", tran.Investigation);
            param[11] = new ReportParameter("ActionTaken", tran.ActionTaken);
            param[12] = new ReportParameter("Recommendation", tran.Recommendation);
            param[13] = new ReportParameter("PreparedBy", from);

            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}