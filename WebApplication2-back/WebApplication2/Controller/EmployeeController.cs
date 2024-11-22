using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeeAsync()
        {
            var allEmployees = await _employeeRepository.GetAllAsync();
            return Ok(allEmployees);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Employee?>> GetByIdEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee)
        {
            if(id != employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return Ok(employee);//CreatedAtAction(nameof(GetByIdEmployeeAsync), new { id = employee.Id }, employee);
        }

        [HttpPost] 
        public async Task<ActionResult<Employee>> CreateEmployeeAsync(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _employeeRepository.AddEmployeeAsync(employee);
            return Ok(employee);//CreatedAtAction(nameof(GetByIdEmployeeAsync), new {id = employee.Id}, employee);
        }
    }
}
