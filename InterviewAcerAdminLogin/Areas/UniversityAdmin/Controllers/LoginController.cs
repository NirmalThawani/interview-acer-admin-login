using InterviewAcerAdminLogin.Common.ResponseClasses;
using InterviewAcerAdminLogin.Models;
using InterviewAcerAdminLogin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InterviewAcerAdminLogin.Areas.UniversityAdmin.Controllers
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
            var loginSuccess = await PerformLoginActions(model.LoginUserName, model.LoginPassword);
            if (loginSuccess)
            {
                return RedirectToAction("Index", "Users");
            }

            ModelState.Clear();
            ModelState.AddModelError("", "The username or password is incorrect");
            return View("Login", model);
        }

        // Register methods go here, removed for brevity

        private async Task<bool> PerformLoginActions(string email, string password)
        {
            LoginResponse response = await _loginService.Login(email, password);

            if (response != null && response.IsUniAdmin == "Yes")
            {
                HttpContext.Session.Add("Role", "IsUniAdmin");
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