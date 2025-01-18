using Microsoft.EntityFrameworkCore;

public class HRMSContext : DbContext
{
    public HRMSContext(DbContextOptions<HRMSContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserManagement> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Leaves)
            .WithOne(l => l.Employee)
            .HasForeignKey(l => l.EmployeeId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Payrolls)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Attendances)
            .WithOne(a => a.Employee)
            .HasForeignKey(a => a.EmployeeId);

        modelBuilder.Entity<Role>()
            .HasMany(r => r.Employees)
            .WithOne()
            .HasForeignKey(e => e.RoleId);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Salary)
            .HasColumnType("decimal(18,2)");

        // Sample data seeding
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "User" }
        );

        modelBuilder.Entity<UserManagement>().HasData(
            new UserManagement { Id = 1, Username = "admin", PasswordHash = "hashedpassword", RoleId = 1 },
            new UserManagement { Id = 2, Username = "user", PasswordHash = "hashedpassword", RoleId = 2 }
        );

        // Adding sample employees
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "John Doe", Email = "john.doe@example.com", RoleId = 1 },
            new Employee { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", RoleId = 2 }

        );

        // Adding sample leaves
        modelBuilder.Entity<Leave>().HasData(
            new Leave { Id = 1, EmployeeId = 1, LeaveType = "Sick", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3), Status = "Approved" },
            new Leave { Id = 2, EmployeeId = 2, LeaveType = "Vacation", StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(10), Status = "Pending" }

        );

        // Adding sample payrolls
        modelBuilder.Entity<Payroll>().HasData(
            new Payroll { Id = 1, EmployeeId = 1, Salary = 5000, PayDate = DateTime.Now },
            new Payroll { Id = 2, EmployeeId = 2, Salary = 4500, PayDate = DateTime.Now }
        );
    }

    public decimal CalculatePayroll(int employeeId, DateTime payDate)
    {
        var payroll = Payrolls
            .Where(p => p.EmployeeId == employeeId && p.PayDate == payDate)
            .Select(p => p.Salary)
            .FirstOrDefault();

        // Additional calculations can be added here

        return payroll;
    }

    public void ApproveLeave(int leaveId)
    {
        var leave = Leaves.Find(leaveId);
        if (leave != null)
        {
            leave.Status = "Approved";
            SaveChanges();
        }
    }
}
