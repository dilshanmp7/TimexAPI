using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimexServerApp.Models;

namespace TimexServerApp.DataAccess
{
    public interface IFullStackDBDemoContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}