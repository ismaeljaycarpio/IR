using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Security;

namespace IR
{
    public class DBLogger
    {
        public static void Log(string action, string desc, string relatedEntity)
        {
            using(var db = new IRContextDataContext())
            {
                AuditTrail audit = new AuditTrail();
                audit.User = HttpContext.Current.User.Identity.Name;
                audit.Action = action;
                audit.Description = desc;
                audit.AssociatedId = relatedEntity;
                audit.ActionDate = DateTime.Now;

                db.AuditTrails.InsertOnSubmit(audit);
                db.SubmitChanges();
            }
        }
    }
}