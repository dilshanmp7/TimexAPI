using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;
using TimexServerApp.Requests;
using TimexServerApp.Responses;

namespace TimexServerApp.Repositories
{
    public interface IDepartmentRepository
    {
        Task<DepartmentResponse> Get(string name);
        Task<IEnumerable<DepartmentResponse>> Get();
        Task Add(DepartmentRequest department);
        Task Delete(string name);
        Task Update(string name, DepartmentRequest department);
    }
}
