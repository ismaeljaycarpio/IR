using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Drawing;
using AjaxControlToolkit;

namespace IR.ir
{
    public partial class irform : System.Web.UI.Page
    {
        IRContextDataContext dbIR = new IRContextDataContext();
        EHRISDataContext dbEHRIS = new EHRISDataContext();
        UserAccountDataContext dbUser = new UserAccountDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindDropdown();

                //load employee info
                Guid myUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                var user = (from up in dbUser.UserProfiles
                            join p in dbUser.Positions
                            on up.PositionId equals p.Id
                            where 
                            (up.UserId == myUserId)
                            select new
                            {
                                UserId = up.UserId,
                                FullName = up.LastName + " , " + up.FirstName + " " + up.MiddleName,
                                Position = p.Name
                            }).FirstOrDefault(); ;

                txtPreparedBy.Text = user.FullName;
                txtPosition.Text = user.Position;

                txtTicketNo.Text = (2600 + dbIR.IRTransactions.DefaultIfEmpty().Max(x => x == null ? 0 : x.Id) + 1).ToString();

                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                var ajaxActionTaken = HtmlEditorExtender2.AjaxFileUpload;
                ajaxActionTaken.AllowedFileTypes = "jpg,jpeg,png,bmp";
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
                ir.From = txtFrom.Text;
                ir.Subject = txtSubject.Text;
                ir.Room = txtRoom.Text;
                ir.Date = Convert.ToDateTime(txtDate.Text);
                ir.Status = ddlStatus.SelectedValue;
                ir.WhenIncidentHappen = Convert.ToDateTime(txtWhenIncident.Text);
                ir.WhenAware = rblWhenAware.SelectedValue;
                ir.WhoInvolved = txtWhosInvolved.Text;
                ir.WhatHappened = txtWhatHappened.Text;
                ir.Investigation = txtInvestigation.Text;
                ir.ActionTaken = txtActionTaken.Text;
                ir.Recommendation = txtRecommendation.Text;
                ir.PreparedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                ir.StartDate = DateTime.Now;

                dbIR.IRTransactions.InsertOnSubmit(ir);
                dbIR.SubmitChanges();

                //audit trail
                DBLogger.Log("Create", "Created IR with initial status of: " + ir.Status, 
                    ir.TicketNo);

                int tranId = ir.Id;

                //chk for uploaded photos
                if(FileUpload1.HasFiles)
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
                    }
                    dbIR.SubmitChanges();
                }

                //insert departments involved
                foreach(ListItem item in lstDepartments.Items)
                {
                    if(item.Selected)
                    {
                        DepartmentsInvolved dept = new DepartmentsInvolved();
                        dept.IRId = tranId;
                        dept.DepartmentId = Convert.ToInt32(item.Value);
                        dbIR.DepartmentsInvolveds.InsertOnSubmit(dept);
                    }
                }

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
            //dept
            var dept = (from d in dbEHRIS.DEPARTMENTs
                        select new
                        {
                            Id = d.Id,
                            Department = d.Department1
                        }).ToList();

            lstDepartments.DataSource = dept;
            lstDepartments.DataTextField = "Department";
            lstDepartments.DataValueField = "Id";
            lstDepartments.DataBind();

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

        protected void HtmlEditorExtender2_ImageUploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            var ajaxFileupload = (AjaxFileUpload)sender;
            ajaxFileupload.SaveAs(Server.MapPath("~/photo-evidence/") + e.FileName);
            e.PostedUrl = Page.ResolveClientUrl("../photo-evidence/" + e.FileName);
        }

    }
}