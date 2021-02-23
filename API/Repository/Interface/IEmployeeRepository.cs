using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<ViewModels.Register> Get();
        Task<ViewModels.Register> Get(int Id);
        int Create(ViewModels.Register register);
        int Update(ViewModels.Register register, int Id);
        int Delete(int Id);
    }
}
