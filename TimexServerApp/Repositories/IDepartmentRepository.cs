using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;

namespace TimexServerApp.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> Get(int id);
        Task<IEnumerable<Department>> Get();
        Task Add(Department department);
        Task Delete(int id);
        Task Update(int id,Department department);
    }
}
