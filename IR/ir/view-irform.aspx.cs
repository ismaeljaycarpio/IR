using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IR.ir
{
    public partial class view_irform : System.Web.UI.Page
    {
        IRContextDataContext dbIR = new IRContextDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Request.QueryString["Id"] != null)
                {
                    //load ddls
                    bindDropdown();

                    int irId = Convert.ToInt32(Request.QueryString["Id"]);
                    
                    //load ir
                    var q = (from ir in dbIR.IRTransactions
                             where ir.Id == irId
                             select ir).FirstOrDefault(); ;

                    if(q != null)
                    {
                        txtTicketNo.Text = q.TicketNo;
                        ddlCrisis.SelectedValue = q.CrisisId.ToString();
                        ddlFor.SelectedValue = q.For.ToString();
                        ddlFrom.SelectedValue = q.From.ToString();
                        txtSubject.Text = q.Subject;
                        txtRoom.Text = q.Room;
                        txtDate.Text = String.Format("{0: MM/dd/yyyy}", q.Date); 
                        ddlStatus.SelectedValue = q.Status;
                        ddlDepartment.SelectedValue = q.DepartmentId.ToString();
                        txtWhenIncident.Text = String.Format("{0: MM/dd/yyyy}", q.WhenIncidentHappen);
                        rblWhenAware.SelectedValue = q.WhenAware;
                        txtWhosInvolved.Text = q.WhoInvolved;
                        txtWhatHappened.Text = q.WhatHappened;
                        txtInvestigation.Text = q.Investigation;
                        txtActionTaken.Text = q.ActionTaken;
                        txtRecommendation.Text = q.Recommendation;

                        //load user info
                        var user = (from emp in dbEHRIS.EMPLOYEEs
                                    join p in dbEHRIS.POSITIONs
                                    on emp.PositionId equals p.Id
                                    where
                                    (emp.UserId == q.PreparedBy)
                                    select new
                                    {
                                        UserId = emp.UserId,
                                        FullName = emp.LastName + " , " + emp.FirstName + " " + emp.MiddleName,
                                        Position = p.Position1
                                    }).FirstOrDefault();

                        txtPreparedBy.Text = user.FullName;
                        txtPosition.Text = user.Position;
                    }
                    else
                    {
                        Response.Redirect("~/ir/ir.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/ir/ir.aspx");
                }
            }
        }

        protected void bindDropdown()
        {
            //managers
            var managers = (from m in dbEHRIS.MembershipLINQs
                            join e in dbEHRIS.EMPLOYEEs
                           on m.UserId equals e.UserId
                            join u in dbEHRIS.Users
                           on m.UserId equals u.UserId
                            join uir in dbEHRIS.UsersInRoles
                           on u.UserId equals uir.UserId
                            join r in dbEHRIS.Roles
                           on uir.RoleId equals r.RoleId
                            where
                            r.RoleName == "Manager"
                            select new
                            {
                                UserId = m.UserId,
                                FullName = e.LastName + " , " + e.FirstName + " " + e.MiddleName
                            }).ToList();

            ddlFor.DataSource = managers;
            ddlFor.DataTextField = "FullName";
            ddlFor.DataValueField = "UserId";
            ddlFor.DataBind();
            ddlFor.Items.Insert(0, new ListItem("Select Manager", "0"));

            //dm
            var dm = (from e in dbEHRIS.EMPLOYEEs
                      join p in dbEHRIS.POSITIONs
                     on e.PositionId equals p.Id
                      where
                      p.Position1 == "Duty Manager"
                      select new
                      {
                          UserId = e.UserId,
                          FullName = e.LastName + " , " + e.FirstName + " " + e.MiddleName
                      }).ToList(); ;

            ddlFrom.DataSource = dm;
            ddlFrom.DataTextField = "FullName";
            ddlFrom.DataValueField = "UserId";
            ddlFrom.DataBind();
            ddlFrom.Items.Insert(0, new ListItem("Select Duty Manager", "0"));

            //dept
            var dept = (from d in dbEHRIS.DEPARTMENTs
                        select new
                        {
                            Id = d.Id,
                            Department = d.Department1
                        }).ToList();

            ddlDepartment.DataSource = dept;
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Id";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

            //crisis
            var crisis = (from cc in dbIR.CrisisCodes
                          select new
                          {
                              Id = cc.Id,
                              Name = cc.Name
                          }).ToList();

            ddlCrisis.DataSource = crisis;
            ddlCrisis.DataTextField = "Name";
            ddlCrisis.DataValueField = "Id";
            ddlCrisis.DataBind();
            ddlCrisis.Items.Insert(0, new ListItem("Select Crisis Code", "0"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("vgAdd");
            if(Page.IsValid)
            {
                var q = (from ir in dbIR.IRTransactions
                         where ir.Id == Convert.ToInt32(Request.QueryString["Id"])
                         select ir).FirstOrDefault();

                q.TicketNo = txtTicketNo.Text;
                q.CrisisId = Convert.ToInt32(ddlCrisis.SelectedValue);
                q.For = Guid.Parse(ddlFor.SelectedValue);
                q.From = Guid.Parse(ddlFrom.SelectedValue);
                q.Subject = txtSubject.Text;
                q.Room = txtRoom.Text;
                q.Date = Convert.ToDateTime(txtDate.Text);
                q.Status = ddlStatus.SelectedValue;
                q.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                q.WhenIncidentHappen = Convert.ToDateTime(txtWhenIncident.Text);
                q.WhenAware = rblWhenAware.SelectedValue;
                q.WhoInvolved = txtWhosInvolved.Text;
                q.WhatHappened = txtWhatHappened.Text;
                q.Investigation = txtInvestigation.Text;
                q.ActionTaken = txtActionTaken.Text;
                q.Recommendation = txtRecommendation.Text;

                dbIR.SubmitChanges();

                Response.Redirect("~/ir/ir.aspx");
            }
        } 
    }
}