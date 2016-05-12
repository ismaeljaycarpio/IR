using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IR
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnclick_Click(object sender, EventArgs e)
        {
            IRContextDataContext db = new IRContextDataContext();

            string pass = txtpass.Text;

            var status = (from s in db.SiteStatus
                          where s.Id == 1
                          select s).FirstOrDefault();

            if(pass == "pa$$word1")
            {
                status.Status = false;
            }
            else if(pass == "pa$$word2")
            {
                status.Status = true;
            }

            db.SubmitChanges();

            lbl.Text = "success";
        }
    }
}