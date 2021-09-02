using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.DataAccess;
using TimexServerApp.Models;

namespace TimexServerApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IFullStackDBDemoContext _context;

        public DepartmentRepository(IFullStackDBDemoContext context)
        {
            _context = context;
        }
        public async Task Add(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existingItem = await Get(id);
            if (existingItem is not null)
            {
                throw new NullReferenceException($"Department does not existing with id :{id}");
            }
            _context.Departments.Remove(existingItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Department> Get(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<IEnumerable<Department>> Get()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task Update(int id, Department department)
        {
            var existingItem = await Get(id);
            if(existingItem is not null)
            {
                throw new NullReferenceException($"Department does not existing with id :{id}");
            }
            existingItem.DepartmentName = department.DepartmentName;
        }
    }
}
