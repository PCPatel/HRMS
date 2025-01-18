using System.Collections.Generic;

public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; } // Marked as required
    public required string Email { get; set; } // Marked as required
    public int RoleId { get; set; } // Foreign key property

    public ICollection<Leave> Leaves { get; set; } = new List<Leave>(); // Initialize the collection
    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>(); // Initialize the collection
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>(); // Initialize the collection
}
