﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR.admin
{
    public partial class user_access : System.Web.UI.Page
    {
        EHRISDataContext dbEHRIS = new EHRISDataContext();
        IRContextDataContext dbIR = new IRContextDataContext();
        UserAccountDataContext dbUser = new UserAccountDataContext();
        DAL.AccountManagement accnt = new DAL.AccountManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.gvUsers.DataBind();

                checkRoles();

                //fill roles
                var roles = (from r in dbUser.Roles
                             where
                             r.RoleName == "can-create-ir" ||
                             r.RoleName == "Admin-IR"
                             select r).ToList();

                ddlRoles.DataSource = roles;
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "RoleId";
                ddlRoles.DataBind();

                ddlRoles.Items.Insert(0, new ListItem("-- Select a Role --", "0"));


                ddlCreateRoles.DataSource = roles;
                ddlCreateRoles.DataTextField = "RoleName";
                ddlCreateRoles.DataValueField = "RoleId";
                ddlCreateRoles.DataBind();

                ddlCreateRoles.Items.Insert(0, new ListItem("-- Select a Role --", "0"));


                //fill positions
                var positions = (from p in dbUser.Positions select p).ToList();

                ddlCreatePosition.DataSource = positions;
                ddlCreatePosition.DataTextField = "Name";
                ddlCreatePosition.DataValueField = "Id";
                ddlCreatePosition.DataBind();
                ddlCreatePosition.Items.Insert(0, new ListItem("-- Select Position --", "0"));


                ddlEditPosition.DataSource = positions;
                ddlEditPosition.DataTextField = "Name";
                ddlEditPosition.DataValueField = "Id";
                ddlEditPosition.DataBind();
                ddlEditPosition.Items.Insert(0, new ListItem("-- Select Position --", "0"));
                txtSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gvUsers.DataBind();
            txtSearch.Focus();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lblStatus");
                LinkButton lnkReset = (LinkButton)e.Row.FindControl("lblReset");
                LinkButton lbtnLockedOut = (LinkButton)e.Row.FindControl("lbtnLockedOut");
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                string userName = ((LinkButton)e.Row.FindControl("lblUsername") as LinkButton).Text;

                if (lnkStatus.Text == "Active")
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to deactivate this user ? ');");
                }
                else
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to activate this user ? ');");
                }

                lnkReset.Attributes.Add("onclick", "return confirm('Do you want to reset the password of this user ? ');");

                if (lbtnLockedOut.Text == "Yes")
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Unlock this user ? ');");
                }
                else
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Lock this user ? ');");
                }

                //cant delete your own account
                if (userName == Page.User.Identity.Name)
                {
                    btnDelete.Visible = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                int _TotalRecs = rowCount();
                int _CurrentRecStart = gvUsers.PageIndex * gvUsers.PageSize + 1;
                int _CurrentRecEnd = gvUsers.PageIndex * gvUsers.PageSize + gvUsers.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRole"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                //load user info
                lblUserId.Text = gvUsers.DataKeys[index].Value.ToString();
                lblUserName.Text = (gvUsers.Rows[index].FindControl("lblUsername") as LinkButton).Text;

                //set selected role
                var ro = (from m in dbUser.MembershipLINQs
                          join u in dbUser.Users
                          on m.UserId equals u.UserId
                          join up in dbUser.UserProfiles
                          on u.UserId equals up.UserId
                          join p in dbUser.Positions
                          on up.PositionId equals p.Id
                          join uir in dbUser.UsersInRoles
                          on up.UserId equals uir.UserId
                          join r in dbUser.Roles
                          on uir.RoleId equals r.RoleId
                          where
                          u.UserName == lblUserName.Text
                          select new
                          {
                              FirstName = up.FirstName,
                              MiddleName = up.MiddleName,
                              LastName = up.LastName,
                              RoleId = r.RoleId,
                              PositionId = up.PositionId
                          }).ToList();

                if (ro.Count > 0)
                {
                    var user = ro.FirstOrDefault();

                    txtEditFirstName.Text = user.FirstName;
                    txtEditMiddleName.Text = user.MiddleName;
                    txtEditLastName.Text = user.LastName;
                    ddlRoles.SelectedValue = user.RoleId.ToString();
                    ddlEditPosition.SelectedValue = user.PositionId.ToString();
                }
                else
                {
                    ddlRoles.SelectedValue = "0";
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRole').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteUser"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblDeleteUserId.Text = gvUsers.DataKeys[index].Value.ToString();
                lblDeleteUsername.Text = (gvUsers.Rows[index].FindControl("lblUsername") as LinkButton).Text;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteUser').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void lblStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkStatus = sender as LinkButton;
            GridViewRow gvrow = lnkStatus.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            if (lnkStatus.Text == "Active")
            {
                accnt.DeactivateUser(UserId);

                //audit trail
                DBLogger.Log("Update", "Deactivate User ", Membership.GetUser(UserId).UserName);
            }
            else
            {
                accnt.ActivateUser(UserId);

                //audit trail
                DBLogger.Log("Update", "Activate User ", Membership.GetUser(UserId).UserName);
            }
            this.gvUsers.DataBind();
        }

        protected void lbtnLockedOut_Click(object sender, EventArgs e)
        {
            LinkButton lbtnLockedOut_Click = sender as LinkButton;
            GridViewRow gvrow = lbtnLockedOut_Click.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            MembershipUser getUser = Membership.GetUser(UserId);

            if (lbtnLockedOut_Click.Text == "Yes")
            {
                //unlock
                getUser.UnlockUser();

                //audit trail
                DBLogger.Log("Update", "Unlock User", Membership.GetUser(UserId).UserName);
            }
            else
            {
                //lock
                accnt.LockUser(UserId);

                //audit trail
                DBLogger.Log("Update", "Lock User", Membership.GetUser(UserId).UserName);
            }

            this.gvUsers.DataBind();
        }

        protected void lblReset_Click(object sender, EventArgs e)
        {
            LinkButton lnkReset = sender as LinkButton;
            GridViewRow gvrow = lnkReset.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            //pswd resets to own username
            accnt.ResetPassword(UserId);
            this.gvUsers.DataBind();

            //audit trail
            DBLogger.Log("Reset Password", "Reset Password", Membership.GetUser(UserId).UserName);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var user = (from us in dbUser.UserProfiles
                        where us.UserId == Guid.Parse(lblUserId.Text)
                        select us).FirstOrDefault();

            user.FirstName = txtEditFirstName.Text;
            user.MiddleName = txtEditMiddleName.Text;
            user.LastName = txtEditLastName.Text;
            user.PositionId = Convert.ToInt32(ddlEditPosition.SelectedValue);

            //save to db
            dbUser.SubmitChanges();

            //update roles
            Roles.RemoveUserFromRoles(lblUserName.Text, Roles.GetRolesForUser(lblUserName.Text));

            Roles.AddUserToRole(lblUserName.Text, ddlRoles.SelectedItem.Text);

            //re-load gridview
            this.gvUsers.DataBind();

            //audit trail
            DBLogger.Log("Update", "Updated User Details", user.User.UserName);

            //close modal
            Javascript.HideModal(this, this, "editRole");
        }

        protected void UserDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string strSearch = txtSearch.Text;

            var q = (from m in dbUser.MembershipLINQs
                     join u in dbUser.Users
                     on m.UserId equals u.UserId
                     join up in dbUser.UserProfiles
                     on u.UserId equals up.UserId
                     join p in dbUser.Positions
                     on up.PositionId equals p.Id
                     join uir in dbUser.UsersInRoles
                     on up.UserId equals uir.UserId
                     join r in dbUser.Roles
                     on uir.RoleId equals r.RoleId
                     where
                     (r.RoleName == "can-create-ir" ||
                             r.RoleName == "Admin-IR") &&
                     (up.FirstName.Contains(strSearch) ||
                        up.MiddleName.Contains(strSearch) ||
                        up.LastName.Contains(strSearch) ||
                        u.UserName.Contains(strSearch))
                     select new
                     {
                         UserId = m.UserId,
                         Username = u.UserName,
                         FullName = up.LastName + " , " + up.FirstName + " " + up.MiddleName,
                         RoleName = r.RoleName,
                         Position = p.Name,
                         IsApproved = m.IsApproved,
                         IsLockedOut = m.IsLockedOut
                     }).ToList();

            q = q.OrderByDescending(o => o.RoleName).ToList();
            e.Result = q;
        }

        private int rowCount()
        {
            string strSearch = txtSearch.Text;

            var q = (from m in dbUser.MembershipLINQs
                     join u in dbUser.Users
                     on m.UserId equals u.UserId
                     join up in dbUser.UserProfiles
                     on u.UserId equals up.UserId
                     join p in dbUser.Positions
                     on up.PositionId equals p.Id
                     join uir in dbUser.UsersInRoles
                     on up.UserId equals uir.UserId
                     join r in dbUser.Roles
                     on uir.RoleId equals r.RoleId
                     where
                     (r.RoleName == "can-create-ir" ||
                             r.RoleName == "Admin-IR") &&
                     (up.FirstName.Contains(strSearch) ||
                        up.MiddleName.Contains(strSearch) ||
                        up.LastName.Contains(strSearch) ||
                        u.UserName.Contains(strSearch))
                     select new
                     {
                         UserId = m.UserId,
                         Username = u.UserName,
                         FullName = up.LastName + " , " + up.FirstName + " " + up.MiddleName,
                         RoleName = r.RoleName,
                         IsApproved = m.IsApproved,
                         IsLockedOut = m.IsLockedOut
                     }).ToList();

            q = q.OrderByDescending(o => o.RoleName).ToList();
            return q.Count;
        }

        protected void checkRoles()
        {
            if (!Roles.RoleExists("can-create-ir"))
            {
                Roles.CreateRole("can-create-ir");
            }


            if (!Roles.RoleExists("Admin-IR"))
            {
                Roles.CreateRole("Admin-IR");
            }
        }

        protected void openCreateAccount_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#createUser').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            //chk if username already exist
            if (Membership.GetUser(txtCreateUsername.Text) != null)
            {
                lblErrorMsg.Text = "Username already exist.";
            }
            else
            {
                //register user
                Membership.CreateUser(txtCreateUsername.Text,
                    txtCreateUsername.Text.Trim());

                //add to user profile
                UserProfile user = new UserProfile();
                user.UserId = Guid.Parse(Membership.GetUser(txtCreateUsername.Text).ProviderUserKey.ToString());
                user.FirstName = txtCreateFirstName.Text;
                user.MiddleName = txtCreateMiddleName.Text;
                user.LastName = txtCreateLastName.Text;
                user.PositionId = Convert.ToInt32(ddlCreatePosition.SelectedValue);
                dbUser.UserProfiles.InsertOnSubmit(user);
                dbUser.SubmitChanges();

                //assign role
                Roles.AddUserToRole(txtCreateUsername.Text, ddlCreateRoles.SelectedItem.Text);

                //re-load users
                this.gvUsers.DataBind();

                //audit trail
                DBLogger.Log("Create", "Add User", user.User.UserName);

                //hide modal
                Javascript.HideModal(this, this, "createUser");
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            Membership.DeleteUser(lblDeleteUsername.Text);
            this.gvUsers.DataBind();

            //audit trail
            DBLogger.Log("Delete", "Delete User", lblDeleteUsername.Text);

            //hide modal
            Javascript.HideModal(this, this, "deleteUser");
        }
    }
}