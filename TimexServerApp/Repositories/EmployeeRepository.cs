using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.DataAccess;
using TimexServerApp.Models;

namespace TimexServerApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IFullStackDBDemoContext _context;
        public EmployeeRepository(IFullStackDBDemoContext context)
        {
            _context = context;
        }

        public async Task Add(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existingItem = await Get(id);
            if (existingItem is not null)
            {
                throw new NullReferenceException($"Employee does not existing with id :{id}");
            }
            _context.Employees.Remove(existingItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> Get(int id)
        {
            return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(d =>
                d.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await _context.Employees.Include(d=>d.Department).ToListAsync();
        }

        public async Task Update(int id, Employee employee)
        {
            var existingItem = await Get(id);
            if (existingItem is not null)
            {
                throw new NullReferenceException($"Employee does not existing with id :{id}");
            }
            existingItem.EmployeeName = employee.EmployeeName;
            existingItem.DateOfJoining = employee.DateOfJoining;
            existingItem.PhotoFileName = employee.PhotoFileName;
            existingItem.DepartmentId = employee.DepartmentId;
        }
    }
}
