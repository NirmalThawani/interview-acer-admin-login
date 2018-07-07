using System.Web.Mvc;

namespace InterviewAcerAdminLogin.CustomAtribute
{
    public class AuthenticationAttribute: ActionFilterAttribute
    {
        private readonly TokenContainer tokenContainer;

        public AuthenticationAttribute()
        {
            tokenContainer = new TokenContainer();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (tokenContainer.ApiToken == null)
            {
                filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
            }
        }
    }
}