namespace E2EDemoUserRegistration.Model;

public class LoginResponseDto
{
    public string Token { get; set; }
    public int UserId { get; set; }      // User ID (not Id)
    public string Username { get; set; }
    public string Email { get; set; }
    public int TenantId { get; set; }    // Tenant ID
}