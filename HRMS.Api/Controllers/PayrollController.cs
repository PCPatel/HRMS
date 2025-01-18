using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for EntityState
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization; // Required for authorization

[Route("api/[controller]")]
[ApiController]
[Authorize] // Require authentication for all actions in this controller
public class PayrollController : ControllerBase
{
    private readonly HRMSContext _context;

    public PayrollController(HRMSContext context)
    {
        _context = context;
    }

    // GET: api/Payroll
    [HttpGet]
    public ActionResult<IEnumerable<Payroll>> GetPayrolls()
    {
        return _context.Payrolls.ToList();
    }

    // GET: api/Payroll/5
    [HttpGet("{id}")]
    public ActionResult<Payroll> GetPayroll(int id)
    {
        var payroll = _context.Payrolls.Find(id);
        if (payroll == null)
        {
            return NotFound();
        }
        return payroll;
    }

    // POST: api/Payroll
    [HttpPost]
    public ActionResult<Payroll> PostPayroll(Payroll payroll)
    {
        _context.Payrolls.Add(payroll);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPayroll), new { id = payroll.Id }, payroll);
    }

    // PUT: api/Payroll/5
    [HttpPut("{id}")]
    public IActionResult PutPayroll(int id, Payroll payroll)
    {
        if (id != payroll.Id)
        {
            return BadRequest();
        }

        _context.Entry(payroll).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Payroll/5
    [HttpDelete("{id}")]
    public IActionResult DeletePayroll(int id)
    {
        var payroll = _context.Payrolls.Find(id);
        if (payroll == null)
        {
            return NotFound();
        }

        _context.Payrolls.Remove(payroll);
        _context.SaveChanges();

        return NoContent();
    }
}
