using InterviewAcerAdminLogin.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using InterviewAcerAdminLogin.Service;
using InterviewAcerAdminLogin.Common.ResponseClasses;

namespace InterviewAcerAdminLogin.Controllers
{
    public class LoginController : Controller
    {
        private readonly TokenContainer _tokenContainer;
        private readonly LoginService _loginService;

        public LoginController()
        {
            _tokenContainer = new TokenContainer();
            _loginService = new LoginService();
        }
        // GET: Login
        public ActionResult Index()
        {
            var loginModel = new Login();
            return View("Login", loginModel);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateLogin(Login model)
        {
            var loginSuccess = await PerformLoginActions(model.UserName, model.Password);
            if (loginSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.Clear();
            ModelState.AddModelError("", "The username or password is incorrect");
            return View("Login", model);
        }

        // Register methods go here, removed for brevity

        private async Task<bool> PerformLoginActions(string email, string password)
        {
            LoginResponse response = await _loginService.Login(email, password);
            
            if (response != null && response.IsAdmin == "Yes")
            {
                _tokenContainer.ApiToken = response.AccessToken;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}