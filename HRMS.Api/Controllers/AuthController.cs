using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// Controller for handling authentication-related actions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly HRMSContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(HRMSContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token and a refresh token.
    /// </summary>
    /// <param name="userLogin">The user login details.</param>
    /// <returns>A JWT token and a refresh token if authentication is successful.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto userLogin)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == userLogin.Username && u.PasswordHash == userLogin.Password);
        if (user == null)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        // Generate a refresh token
        var refreshToken = Guid.NewGuid().ToString(); // Example of a simple refresh token

        // Save the refresh token to the database
        user.RefreshToken = refreshToken;
        _context.SaveChanges();

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = refreshToken
        });
    }

    /// <summary>
    /// Refreshes the JWT token using a refresh token.
    /// </summary>
    /// <returns>A new JWT token if the refresh is successful.</returns>
    [HttpPost("refresh")]
    public IActionResult Refresh(string refreshToken)
    {
        // Get the JWT token from the Authorization header
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if (authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Validate the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null) //  || jwtToken.ValidTo <= DateTime.UtcNow
            {
                return Unauthorized();
            }

            var username = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            // Validate the refresh token against the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || user.RefreshToken != refreshToken) // Assuming the refresh token is stored as Id
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var newToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            // Generate a new refresh token
            var newRefreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = newRefreshToken; // Update the refresh token in the database
            _context.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(newToken),
                refreshToken = newRefreshToken
            });
        }

        return Unauthorized();
    }
}

/// <summary>
/// Data transfer object for user login.
/// </summary>
public class UserLoginDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
