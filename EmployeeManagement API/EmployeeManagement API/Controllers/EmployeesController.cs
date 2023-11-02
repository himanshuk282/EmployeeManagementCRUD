using EmployeeManagement_API.Data;
using EmployeeManagement_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeManagementDbContext _employeeManagementDbContext;
        public EmployeesController(EmployeeManagementDbContext employeeManagementDbContext)
        {
            this._employeeManagementDbContext = employeeManagementDbContext;            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeManagementDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _employeeManagementDbContext.Employees.AddAsync(employeeRequest);
            await _employeeManagementDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeManagementDbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        {
            var employee = await _employeeManagementDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _employeeManagementDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeManagementDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeManagementDbContext.Employees.Remove(employee);
            await _employeeManagementDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
