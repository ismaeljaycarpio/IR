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
                if (!Roles.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }

                if (!Roles.RoleExists("CanApprove"))
                {
                    Roles.CreateRole("CanApprove");
                }

                if (!Roles.RoleExists("CanCreate"))
                {
                    Roles.CreateRole("CanCreate");
                }

                //create admin
                Membership.CreateUser("admin", "pa$$word");
                Roles.AddUserToRole("admin", "Admin");
            }
        }
    }
}