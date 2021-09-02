using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimexServerApp.DataAccess;
using TimexServerApp.Models;
using TimexServerApp.Repositories;
using TimexServerApp.Responses;

namespace TimexServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _imapper;

        public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentRepository departmentRepository, IMapper imapper)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _imapper = imapper;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            _logger.LogInformation($"Started meathod : {MethodBase.GetCurrentMethod().Name}");
            var departments = await _departmentRepository.Get();
            var departmantResponses= _imapper.Map<IEnumerable<DepartmentResponse>>(departments);
            return new JsonResult(departmantResponses);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var department = await _departmentRepository.Get(id);
            var departmantResponse = _imapper.Map<IEnumerable<DepartmentResponse>>(department);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult(departmantResponse);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            await _departmentRepository.Update(id, department);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDepartment(Department department)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _departmentRepository.Add(department);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _departmentRepository.Delete(id);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Deleted Successfully.");
        }

    }
}
