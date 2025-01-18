using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for EntityState
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization; // Required for authorization

[Route("api/[controller]")]
[ApiController]
[Authorize] // Require authentication for all actions in this controller
public class LeaveController : ControllerBase
{
    private readonly HRMSContext _context;

    public LeaveController(HRMSContext context)
    {
        _context = context;
    }

    // GET: api/Leave
    [HttpGet]
    public ActionResult<IEnumerable<Leave>> GetLeaves()
    {
        return _context.Leaves.ToList();
    }

    // GET: api/Leave/5
    [HttpGet("{id}")]
    public ActionResult<Leave> GetLeave(int id)
    {
        var leave = _context.Leaves.Find(id);
        if (leave == null)
        {
            return NotFound();
        }
        return leave;
    }

    // POST: api/Leave
    [HttpPost]
    public ActionResult<Leave> PostLeave(Leave leave)
    {
        _context.Leaves.Add(leave);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetLeave), new { id = leave.Id }, leave);
    }

    // PUT: api/Leave/5
    [HttpPut("{id}")]
    public IActionResult PutLeave(int id, Leave leave)
    {
        if (id != leave.Id)
        {
            return BadRequest();
        }

        _context.Entry(leave).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Leave/5
    [HttpDelete("{id}")]
    public IActionResult DeleteLeave(int id)
    {
        var leave = _context.Leaves.Find(id);
        if (leave == null)
        {
            return NotFound();
        }

        _context.Leaves.Remove(leave);
        _context.SaveChanges();

        return NoContent();
    }
}
