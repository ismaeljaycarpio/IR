﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IR.ir
{
    public partial class irform : System.Web.UI.Page
    {
        IRContextDataContext dbIR = new IRContextDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindDropdown();

                //load employee info
                Guid myUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                var user = (from emp in dbEHRIS.EMPLOYEEs
                            join p in dbEHRIS.POSITIONs
                            on emp.PositionId equals p.Id
                            where 
                            (emp.UserId == myUserId)
                            select new
                            {
                                UserId = emp.UserId,
                                FullName = emp.LastName + " , " + emp.FirstName + " " + emp.MiddleName,
                                Position = p.Position1
                            }).FirstOrDefault(); ;

                txtPreparedBy.Text = user.FullName;
                txtPosition.Text = user.Position;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("vgAdd");
            if(Page.IsValid)
            {
                IRTransaction ir = new IRTransaction();
                ir.TicketNo = txtTicketNo.Text;
                ir.CrisisId = Convert.ToInt32(ddlCrisis.SelectedValue);
                ir.For = Guid.Parse(ddlFor.SelectedValue);
                ir.From = Guid.Parse(ddlFrom.SelectedValue);
                ir.Subject = txtSubject.Text;
                ir.Room = txtRoom.Text;
                ir.Date = Convert.ToDateTime(txtDate.Text);
                ir.Status = ddlStatus.SelectedItem.Text;
                ir.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ir.WhenIncidentHappen = Convert.ToDateTime(txtWhenIncident.Text);
                ir.WhenAware = rblWhenAware.SelectedValue;
                ir.WhoInvolved = txtWhosInvolved.Text;
                ir.WhatHappened = txtWhatHappened.Text;
                ir.Investigation = txtInvestigation.Text;
                ir.ActionTaken = txtActionTaken.Text;
                ir.Recommendation = txtRecommendation.Text;
                ir.PreparedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                dbIR.IRTransactions.InsertOnSubmit(ir);
                dbIR.SubmitChanges();

                Response.Redirect("~/ir/ir.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ir/ir.aspx");
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
    }
}