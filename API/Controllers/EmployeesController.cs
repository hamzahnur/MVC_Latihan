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
    public class EmployeesController : ApiController
    {
        EmployeeRepository repository = new EmployeeRepository();
        public IEnumerable<ViewModels.Register> Get()
        {
            return repository.Get();

        }
        public IHttpActionResult Post(ViewModels.Register register)
        {
            var cek = repository.Create(register);
            if (cek == 0)
            {
                return BadRequest("Data gagal di tambah!");
            }
            else
            {
                return Ok("Data berhasil ditambah");
            }
        }
        public IHttpActionResult Delete(int Id)
        {
            var cek = repository.Delete(Id);
            if (cek == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok("Data Sudah dihapus");
            }
        }
        public IHttpActionResult Put(ViewModels.Register register, int Id)
        {
            var cek = repository.Update(register, Id);
            if (cek == 0)
            {
                return BadRequest("Data do not update!!!");
            }
            else
            {
                return Ok("Data has been update");
            }
        }
        public Task<ViewModels.Register> Get(int Id)
        {
            return repository.Get(Id); 
        }
    }
}
