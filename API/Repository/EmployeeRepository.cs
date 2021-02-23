using API.Models;
using API.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        public int Create(ViewModels.Register register)
        {
            var SP_Name = "SP_Register";
            parameters.Add("@Name", register.Name);
            parameters.Add("@Address", register.Address);
            parameters.Add("@Email", register.Email);
            parameters.Add("@Phone", register.Phone);
            parameters.Add("@Password", register.Password);
            var Result = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return Result;
        }

        public int Delete(int Id)
        {
            var SP_Name = "SP_DeleteEmployee";
            parameters.Add("@ID", Id);
            var Result = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return Result;
        }

        public IEnumerable<ViewModels.Register> Get()
        {
            var SP_Name = "SP_ReteriveEmployee";
            var Result = connection.Query<ViewModels.Register>(SP_Name, commandType: CommandType.StoredProcedure);
            return Result;
        }

        public async Task<ViewModels.Register> Get(int Id)
        {
            var SP_Name = "SP_ReteriveEmployeeById";
            parameters.Add("@id", Id);
            var Result = await connection.QueryAsync<ViewModels.Register>(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return Result.FirstOrDefault();
        }

        public int Update(ViewModels.Register register, int Id)
        {
            var SP_Name = "SP_UpdateEmployee";
            parameters.Add("@ID", Id);
            parameters.Add("@Name", register.Name);
            parameters.Add("@Address", register.Address);
            parameters.Add("@Email", register.Email);
            parameters.Add("@Phone", register.Phone);
            parameters.Add("@Password", register.Password);
            var Result = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return Result;
        }
    }
}