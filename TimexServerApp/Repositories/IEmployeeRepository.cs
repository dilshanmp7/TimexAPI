using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;

namespace TimexServerApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<IEnumerable<Employee>> Get();
        Task Add(Employee employee);
        Task Delete(int id);
        Task Update(int id, Employee employee);
    }
}
