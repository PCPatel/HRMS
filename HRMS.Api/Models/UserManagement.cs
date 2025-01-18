public class UserManagement
{
    public int Id { get; set; }
    public required string Username { get; set; } // Marked as required
    public required string PasswordHash { get; set; } // Marked as required
    public int RoleId { get; set; }

    public Role Role { get; set; } // Navigation property

    // New property for storing the refresh token
    public string? RefreshToken { get; set; } // Nullable to allow for users without a refresh token
}
