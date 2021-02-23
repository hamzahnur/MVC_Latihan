using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Latihan.Controllers
{
    public class EmployeeController : Controller
    {
        readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:44377/API/") };
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<API.ViewModels.Register> employees = null;
            var respondTask = client.GetAsync("Employees");
            respondTask.Wait();
            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<API.ViewModels.Register>>();
                readTask.Wait();
                employees = readTask.Result;
            }
            return View(employees);
        }
        public ActionResult Details(int Id)
        {
            IEnumerable<API.ViewModels.Register> employees = null;
            var respondTask = client.GetAsync("Employees");
            respondTask.Wait();
            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<API.ViewModels.Register>>();
                readTask.Wait();
                employees = readTask.Result;
            }

            return View(employees.FirstOrDefault(s => s.Id == Id));
        }
        public ActionResult Edit(int Id)
        {
            IEnumerable<API.ViewModels.Register> employees = null;
            var respondTask = client.GetAsync("Employees");
            respondTask.Wait();
            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<API.ViewModels.Register>>();
                readTask.Wait();
                employees = readTask.Result;
            }

            return View(employees.FirstOrDefault(s => s.Id == Id));
        }

        [HttpPost]
        public ActionResult Edit(API.ViewModels.Register register, int Id)
        {
            HttpResponseMessage response = client.PutAsJsonAsync<API.ViewModels.Register>("Employees/" + Id, register).Result;
            return RedirectToAction("Details/" + Id);
        }
        public ActionResult Delete(int Id)
        {

            IEnumerable<API.ViewModels.Register> suppliers = null;
            var respondTask = client.GetAsync("Employees/" + Id.ToString());
            respondTask.Wait();
            var result = respondTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<API.ViewModels.Register>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            return View(suppliers.FirstOrDefault(s => s.Id == Id));
        }

        [HttpPost]
        public ActionResult Delete(API.ViewModels.Register register, int Id)
        {
            var deleteTask = client.DeleteAsync("Employees/" + Id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(API.ViewModels.Register register)
        {
            HttpResponseMessage respone = client.PostAsJsonAsync("Employees", register).Result;
            return RedirectToAction("Login","Account");
        }
    }
}