using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InterviewAcerAdminLogin.CustomAtribute
{
    public class AuthenticationAttribute: ActionFilterAttribute
    {
        private readonly TokenContainer tokenContainer;

        public AuthenticationAttribute()
        {
            tokenContainer = new TokenContainer();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (tokenContainer.ApiToken == null || HttpContext.Current.Session.Contents["Role"] == null || HttpContext.Current.Session.Contents["Role"].ToString() != "Admin")
                {
                    filterContext.Result = new RedirectResult("~/Login");                          
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}