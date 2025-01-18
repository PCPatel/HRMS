public class Payroll
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required decimal Salary { get; set; } // Marked as required
    public required DateTime PayDate { get; set; } // Marked as required

    public Employee Employee { get; set; } // Navigation property
}
