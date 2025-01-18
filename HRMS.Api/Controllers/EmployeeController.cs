using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization; // Required for authorization

[Route("api/[controller]")]
[ApiController]
[Authorize] // Require authentication for all actions in this controller
public class EmployeeController : ControllerBase
{
    private readonly HRMSContext _context;

    public EmployeeController(HRMSContext context)
    {
        _context = context;
    }

    // GET: api/Employee
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetEmployees()
    {
        return _context.Employees.ToList();
    }

    // GET: api/Employee/5
    [HttpGet("{id}")]
    public ActionResult<Employee> GetEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        return employee;
    }

    // POST: api/Employee
    [HttpPost]
    public ActionResult<Employee> PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public IActionResult PutEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        _context.Entry(employee).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        _context.SaveChanges();

        return NoContent();
    }
}
