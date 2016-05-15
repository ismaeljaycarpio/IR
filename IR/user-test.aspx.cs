using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IR
{
    public partial class user_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //create
                if (!Roles.RoleExists("Admin-IR"))
                {
                    Roles.CreateRole("Admin-IR");
                }

                if (!Roles.RoleExists("can-approve-ir"))
                {
                    Roles.CreateRole("can-approve-ir");
                }

                if (!Roles.RoleExists("can-create-ir"))
                {
                    Roles.CreateRole("can-create-ir");
                }

                //create admin
                Membership.CreateUser("admin", "pa$$word");
                Roles.AddUserToRole("admin", "Admin-IR");
            }
        }
    }
}