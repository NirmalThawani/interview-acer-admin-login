using InterviewAcerAdminLogin.CustomAtribute;
using System.Web.Mvc;

namespace InterviewAcerAdminLogin.Controllers
{
    public class HomeController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }
    }
}