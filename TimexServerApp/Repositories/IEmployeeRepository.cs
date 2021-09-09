using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;
using TimexServerApp.Requests;
using TimexServerApp.Responses;

namespace TimexServerApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeResponse> Get(int id);
        Task<IEnumerable<EmployeeResponse>> Get();
        Task Add(EmployeeRequest employee);
        Task Delete(int id);
        Task Update(int id, EmployeeRequest employee);
    }
}
