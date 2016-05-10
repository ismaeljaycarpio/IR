﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;

namespace IR.ir
{
    public partial class approval_irform : System.Web.UI.Page
    {
        IRContextDataContext dbIR = new IRContextDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    //load ddls
                    bindDropdown();

                    int irId = Convert.ToInt32(Request.QueryString["Id"]);

                    //load ir
                    var q = (from ir in dbIR.IRTransactions
                             where ir.Id == irId
                             select ir).FirstOrDefault(); ;

                    if (q != null)
                    {
                        txtTicketNo.Text = q.TicketNo;
                        ddlCrisis.SelectedValue = q.CrisisId.ToString();
                        ddlFrom.SelectedValue = q.From.ToString();
                        txtSubject.Text = q.Subject;
                        txtRoom.Text = q.Room;
                        txtDate.Text = String.Format("{0: MM/dd/yyyy}", q.Date);
                        ddlStatus.SelectedValue = q.Status;
                        ddlDepartment.SelectedValue = q.DepartmentId.ToString();
                        txtWhenIncident.Text = q.WhenIncidentHappen.ToString();
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
                                    (emp.Emp_Id == q.PreparedBy)
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
                        Response.Redirect("~/ir/approval.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/ir/approval.aspx");
                }
            }
        }

        protected void gvImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int Id = Convert.ToInt32(gvImages.DataKeys[index].Value);

                hfDeleteId.Value = Id.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("vgAdd");
            if (Page.IsValid)
            {
                var q = (from ir in dbIR.IRTransactions
                         where ir.Id == Convert.ToInt32(Request.QueryString["Id"])
                         select ir).FirstOrDefault();

                q.TicketNo = txtTicketNo.Text;
                q.CrisisId = Convert.ToInt32(ddlCrisis.SelectedValue);
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

                int tranId = q.Id;

                //uploaded imgs
                if (FileUpload1.HasFiles)
                {
                    foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(Server.MapPath("~/photo-evidence/") + tranId + "_" + fileName);

                        //create thumbnail
                        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("~/photo-evidence/") + tranId + "_" + fileName);
                        System.Drawing.Image bmp1 = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                        bmp1.Save(Server.MapPath("~/photo-evidence/") + tranId + "_" + "thumb_" + fileName);

                        //record to db
                        EvidencePhoto ep = new EvidencePhoto();
                        ep.IrId = tranId;
                        ep.ImagePath = fileName;

                        dbIR.EvidencePhotos.InsertOnSubmit(ep);
                        dbIR.SubmitChanges();
                    }
                }

                Response.Redirect("~/ir/approval.aspx");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int tranId = Convert.ToInt32(Request.QueryString["Id"]);

            var q = (from ep in dbIR.EvidencePhotos
                     where ep.Id == Convert.ToInt32(hfDeleteId.Value)
                     select ep).FirstOrDefault();

            dbIR.EvidencePhotos.DeleteOnSubmit(q);
            dbIR.SubmitChanges();

            this.gvImages.DataBind();

            //delete images in dir
            FileInfo fi = new FileInfo(Server.MapPath("~/photo-evidence/") + tranId + "_" + q.ImagePath);
            fi.Delete();

            FileInfo fiThumb = new FileInfo(Server.MapPath("~/photo-evidence") + tranId + "_" + "thumb_" + q.ImagePath);
            fiThumb.Delete();

            this.Repeater1.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void ImageDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = (from ep in dbIR.EvidencePhotos
                     where ep.IrId == Convert.ToInt16(Request.QueryString["Id"])
                     select ep).ToList();

            e.Result = q;
        }

        protected void ImageSlideShowDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var q = (from ep in dbIR.EvidencePhotos
                     where ep.IrId == Convert.ToInt32(Request.QueryString["Id"])
                     select ep).ToList();

            e.Result = q;
        }

        protected void bindDropdown()
        {
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