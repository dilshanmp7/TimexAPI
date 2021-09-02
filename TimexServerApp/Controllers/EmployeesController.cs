using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _imapper;

        public EmployeesController(ILogger<EmployeesController> logger,IEmployeeRepository employeeRepository, IWebHostEnvironment env, IMapper imapper)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _env = env;
            _imapper = imapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation($"Started meathod : {MethodBase.GetCurrentMethod().Name}");
            var employees = await _employeeRepository.Get();
            var employeeResponse = _imapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return new JsonResult(employeeResponse);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var employee = await _employeeRepository.Get(id);
            var employeeResponse = _imapper.Map<EmployeeResponse>(employee);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult(employeeResponse);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            await _employeeRepository.Update(id, employee);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _employeeRepository.Add(employee);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            await _employeeRepository.Delete(id);
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Deleted Successfully.");
        }


        [HttpPost("SaveFile")]
        public async Task<IActionResult> SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = Path.Combine(_env.ContentRootPath, "Photos", fileName);
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new JsonResult("Anonymous.jpg");
            }
        }

    }
}
