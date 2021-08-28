using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly FullStackDBDemoContext _context;

        public DepartmentsController(ILogger<DepartmentsController> logger, FullStackDBDemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            _logger.LogInformation($"Started meathod : {MethodBase.GetCurrentMethod().Name}");
            return await _context.Departments.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                var message = $"Department not existing with Id {id}";
                _logger.LogError(message);
                return NotFound(message);
            }
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return department;
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

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DepartmentExists(id))
                {
                    _logger.LogError(ex,ex.Message);
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            _logger.LogInformation($"Start meathod : {MethodBase.GetCurrentMethod().Name}");
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                var message = $"Department not existing with Id {id}";
                _logger.LogError(message);
                return NotFound(message);
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"End meathod : {MethodBase.GetCurrentMethod().Name}");
            return new JsonResult("Deleted Successfully.");
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
