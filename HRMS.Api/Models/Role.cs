public class Role
{
    public int Id { get; set; }
    public required string RoleName { get; set; } // Marked as required

    public ICollection<Employee> Employees { get; set; } = new List<Employee>(); // Initialize the collection
}
