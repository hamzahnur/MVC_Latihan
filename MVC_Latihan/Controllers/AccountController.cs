using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Latihan.Controllers
{
    public class AccountController : Controller
    {
        readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:44377/API/") };
        private MyContext myContext = new MyContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(API.ViewModels.Login login)
        {
            
            HttpResponseMessage respone = client.PostAsJsonAsync("Accounts", login).Result;
            IEnumerable<API.ViewModels.Login> logins = null;
            var respondTask = client.GetAsync("Employees");
            respondTask.Wait();
            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<API.ViewModels.Login>>();
                readTask.Wait();
                logins = readTask.Result;
               
            }
            var Id = logins.FirstOrDefault(s => s.Email == login.Email).Id;
            
            if (respone.IsSuccessStatusCode)
            {
                Session["Login"] = login.Email.ToString();
                return RedirectToAction("Details/" + Id.ToString(),"Employee");
            }
            else
            {
                ViewBag.LoginError = "Email atau password salah";
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(API.ViewModels.Login login)
        {
            
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}