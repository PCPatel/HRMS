using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for EntityState
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization; // Required for authorization

[Route("api/[controller]")]
[ApiController]
[Authorize] // Require authentication for all actions in this controller
public class AttendanceController : ControllerBase
{
    private readonly HRMSContext _context;

    public AttendanceController(HRMSContext context)
    {
        _context = context;
    }

    // GET: api/Attendance
    [HttpGet]
    public ActionResult<IEnumerable<Attendance>> GetAttendances()
    {
        return _context.Attendances.ToList();
    }

    // GET: api/Attendance/5
    [HttpGet("{id}")]
    public ActionResult<Attendance> GetAttendance(int id)
    {
        var attendance = _context.Attendances.Find(id);
        if (attendance == null)
        {
            return NotFound();
        }
        return attendance;
    }

    // POST: api/Attendance
    [HttpPost]
    public ActionResult<Attendance> PostAttendance(Attendance attendance)
    {
        _context.Attendances.Add(attendance);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);
    }

    // PUT: api/Attendance/5
    [HttpPut("{id}")]
    public IActionResult PutAttendance(int id, Attendance attendance)
    {
        if (id != attendance.Id)
        {
            return BadRequest();
        }

        _context.Entry(attendance).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Attendance/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAttendance(int id)
    {
        var attendance = _context.Attendances.Find(id);
        if (attendance == null)
        {
            return NotFound();
        }

        _context.Attendances.Remove(attendance);
        _context.SaveChanges();

        return NoContent();
    }
}
