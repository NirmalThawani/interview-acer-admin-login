using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InterviewAcerAdminLogin.Areas.UniversityAdmin.CustomActionFilter
{
    public class UniAdminAuthenticationAttribute : ActionFilterAttribute
    {
        private readonly TokenContainer tokenContainer;

        public UniAdminAuthenticationAttribute()
        {
            tokenContainer = new TokenContainer();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (tokenContainer.ApiToken == null || HttpContext.Current.Session.Contents["Role"] == null || HttpContext.Current.Session.Contents["Role"].ToString() != "IsUniAdmin")
                {
                    filterContext.Result = new RedirectResult("~/UniversityAdmin/Login");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}