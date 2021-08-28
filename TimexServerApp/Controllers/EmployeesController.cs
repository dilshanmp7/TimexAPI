using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimexServerApp.DataAccess;
using TimexServerApp.Models;

namespace TimexServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly FullStackDBDemoContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(ILogger<EmployeesController> logger,
            FullStackDBDemoContext context,IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            _logger.LogInformation($"Started meathod : {MethodBase.GetCurrentMethod().Name}");
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                var message = $"Employee not existing with Id {id}";
                _logger.LogError(message);
                return NotFound(message);
            }
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return employee;
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

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EmployeeExists(id))
                {
                    _logger.LogError(ex, ex.Message);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                var message = $"Employee not existing with Id {id}";
                _logger.LogError(message);
                return NotFound(message);
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
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


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
