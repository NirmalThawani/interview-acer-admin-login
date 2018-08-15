using System.Web.Mvc;

namespace InterviewAcerAdminLogin.Areas.UniversityAdmin
{
    public class UniversityAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UniversityAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UniversityAdmin_default",
                "UniversityAdmin/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                new[] { "InterviewAcerAdminLogin.Areas.UniversityAdmin.Controllers" }
            );
        }
    }
}