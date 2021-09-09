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
using TimexServerApp.Requests;
using TimexServerApp.Responses;

namespace TimexServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            _logger.LogInformation($"Started meathod : {MethodBase.GetCurrentMethod().Name}");
            var departmantResponses = await _departmentRepository.Get();
            return new JsonResult(departmantResponses);
        }

        // GET: api/Departments/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetDepartment(string name)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var departmantResponse = await _departmentRepository.Get(name);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult(departmantResponse);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutDepartment(string name, [FromBody] DepartmentRequest departmentRequest)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _departmentRepository.Update(name, departmentRequest);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody]DepartmentRequest departmentRequest)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _departmentRepository.Add(departmentRequest);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Added Successfully");
        }

        // DELETE: api/Departments/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteDepartment(string name)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _departmentRepository.Delete(name);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Deleted Successfully.");
        }

    }
}
