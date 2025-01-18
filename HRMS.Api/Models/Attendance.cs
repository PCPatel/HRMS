public class Attendance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required DateTime Date { get; set; } // Marked as required
    public required bool IsPresent { get; set; } // Marked as required

    public Employee Employee { get; set; } // Navigation property
}
