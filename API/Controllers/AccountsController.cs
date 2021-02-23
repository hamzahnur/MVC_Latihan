using API.Models;
using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class AccountsController : ApiController
    {
        
        EmployeeRepository employeeRepository = new EmployeeRepository();

        public IHttpActionResult Post(ViewModels.Login login)
        {
            var Email = login.Email;
            var Password = login.Password;

            var result = employeeRepository.Get().FirstOrDefault(r => r.Email == Email && r.Password == Password);

            if (result != null)
            {
                return Ok("Login Berhasil");
            }
            return Content(HttpStatusCode.NotFound, "Data tidak ada");
        }
    }
}
