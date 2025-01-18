public class Leave
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required string LeaveType { get; set; } // Marked as required
    public required DateTime StartDate { get; set; } // Marked as required
    public required DateTime EndDate { get; set; } // Marked as required
    public required string Status { get; set; } // Marked as required

    public Employee Employee { get; set; } // Navigation property
}
