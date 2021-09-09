using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.DataAccess;
using TimexServerApp.Models;
using TimexServerApp.Requests;
using TimexServerApp.Responses;

namespace TimexServerApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IFullStackDBDemoContext _context;
        private readonly IMapper _imapper;

        public DepartmentRepository(IFullStackDBDemoContext context, IMapper imapper)
        {
            _context = context;
            _imapper = imapper;
        }
        public async Task Add(DepartmentRequest departmentRequest)
        {
            var department = _imapper.Map<Department>(departmentRequest);
            var isExist = await _context.Departments.AnyAsync(a => a.DepartmentName.ToLower().Equals(department.DepartmentName.ToLower()));
            if (isExist) {
                throw new Exception($"Department Name '{department.DepartmentName}' already existing");
            }
            else
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(string name)
        {
            var departments = _context.Departments.Where(a=>a.DepartmentName.ToLower().Equals(name.ToLower()));
            if (!departments.Any())
            {
                throw new NullReferenceException($"Department does not existing with name :{name}");
            }
            _context.Departments.RemoveRange(departments);
            await _context.SaveChangesAsync();
        }

        public async Task<DepartmentResponse> Get(string name)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(a => a.DepartmentName.ToLower().Equals(name.ToLower()));
            var departmentResponse = _imapper.Map<DepartmentResponse>(department);
            return departmentResponse;
        }

        public async Task<IEnumerable<DepartmentResponse>> Get()
        {
            var departments = await _context.Departments.ToListAsync();
            var departmentResponses = _imapper.Map<IEnumerable<DepartmentResponse>>(departments);
            return departmentResponses;
        }

        public async Task Update(string name, DepartmentRequest departmentRequest)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(a => a.DepartmentName.ToLower().Equals(name.ToLower()));
            if (department is null)
            {
                throw new NullReferenceException($"Department does not existing with name :{name}");
            }
            department.DepartmentName = departmentRequest.DepartmentName;
            await _context.SaveChangesAsync();
        }
    }
}
