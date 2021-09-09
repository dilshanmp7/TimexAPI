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
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IFullStackDBDemoContext _context;
        private readonly IMapper _imapper;

        public EmployeeRepository(IFullStackDBDemoContext context, IMapper imapper)
        {
            _context = context;
            _imapper = imapper;
        }

        public async Task Add(EmployeeRequest employeeRequest)
        {
            var employee = _imapper.Map<Employee>(employeeRequest);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                throw new NullReferenceException($"Employee does not existing with id :{id}");
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeResponse> Get(int id)
        {
            var employee= await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(d => d.EmployeeId == id);
            var employeeResponse = _imapper.Map<EmployeeResponse>(employee);
            return employeeResponse;
        }

        public async Task<IEnumerable<EmployeeResponse>> Get()
        {
            var employees= await _context.Employees.Include(d=>d.Department).ToListAsync();
            var employeeResponses = _imapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return employeeResponses;
        }

        public async Task Update(int id, EmployeeRequest employeeRequest)
        {
            var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(d => d.EmployeeId == id);
            if (employee is null)
            {
                throw new NullReferenceException($"Employee does not existing with id :{id}");
            }
            employee.EmployeeName = employeeRequest.EmployeeName;
            employee.DepartmentId = employeeRequest.DepartmentId;
            if (employeeRequest.DateOfJoining is not null)
                employee.DateOfJoining = employeeRequest.DateOfJoining;
            if (employeeRequest.PhotoFileName is not null)
                employee.PhotoFileName = employeeRequest.PhotoFileName;
            await _context.SaveChangesAsync();
        }
    }
}
