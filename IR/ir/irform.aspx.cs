using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Drawing;

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
                string myUsername = Membership.GetUser().UserName.ToString();

                var user = (from emp in dbEHRIS.EMPLOYEEs
                            join p in dbEHRIS.POSITIONs
                            on emp.PositionId equals p.Id
                            where 
                            (emp.Emp_Id == myUsername)
                            select new
                            {
                                UserId = emp.UserId,
                                FullName = emp.LastName + " , " + emp.FirstName + " " + emp.MiddleName,
                                Position = p.Position1
                            }).FirstOrDefault(); ;

                txtPreparedBy.Text = user.FullName;
                txtPosition.Text = user.Position;

                txtTicketNo.Text = (2600 + dbIR.IRTransactions.DefaultIfEmpty().Max(x => x == null ? 0 : x.Id) + 1).ToString();
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
                ir.From = Guid.Parse(ddlFrom.SelectedValue);
                ir.Subject = txtSubject.Text;
                ir.Room = txtRoom.Text;
                ir.Date = Convert.ToDateTime(txtDate.Text);
                ir.Status = ddlStatus.SelectedValue;
                ir.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ir.WhenIncidentHappen = Convert.ToDateTime(txtWhenIncident.Text);
                ir.WhenAware = rblWhenAware.SelectedValue;
                ir.WhoInvolved = txtWhosInvolved.Text;
                ir.WhatHappened = txtWhatHappened.Text;
                ir.Investigation = txtInvestigation.Text;
                ir.ActionTaken = txtActionTaken.Text;
                ir.Recommendation = txtRecommendation.Text;
                ir.PreparedBy = Membership.GetUser().UserName.ToString();
                ir.Approval = "Pending";

                dbIR.IRTransactions.InsertOnSubmit(ir);
                dbIR.SubmitChanges();

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
                        dbIR.SubmitChanges();
                    }
                }

                Response.Redirect("~/ir/ir.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ir/ir.aspx");
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

        protected void createPhotoFolder()
        {

        }
    }
}