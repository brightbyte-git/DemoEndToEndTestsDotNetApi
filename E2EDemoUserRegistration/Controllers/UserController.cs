using E2EDemoUserRegistration.Model;
using Microsoft.AspNetCore.Mvc;

namespace E2EDemoUserRegistration.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;


    public UserController()
    {
        /// <summary>/// Registers a new user with the specified registration data and returns a success or failure response.
        /// </summary>
        /// <param name="registrationDto">The registration data containing user details such as email, password, and tenant information.</param>
        /// <returns>An IActionResult indicating whether the user was registered successfully or not with a message in the response body.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            if (registrationDto == null)
                return BadRequest(new { Message = "Invalid registration data." });

            try
            {
                var success = await _userRepository.RegisterUserAsync(registrationDto);

                if (success)
                    return Ok(new
                    {
                        Message = registrationDto.IsInvited
                            ? "User registered successfully with existing tenant."
                            : "User registered successfully with new tenant."
                    });
            }
            catch (InvalidOperationException ex)
            {
                // Handle known exceptions like email or tenant errors
                return Conflict(new { Message = ex.Message }); // 409 Conflict for already existing resources
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }

            return BadRequest(new { Message = "User registration failed." });
        }
    }
    
    /// <summary>
    /// Logs in a user with the specified email and password, returning a JWT token if successful.
    /// </summary>
    /// <param name="loginDto">The login data containing the user's email and password.</param>
    /// <returns>An IActionResult indicating whether the user was logged in successfully or not with a JWT token or an error message in the response body.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var result  = await    _userRepository.LoginAsync(loginDto.Email, loginDto.Password);

        if (result   == null)
        {
            return   Unauthorized(new   { Message = "Invalid email or password." });
        }

        // Use UserId instead of Id if you're accessing the user ID
        return Ok(result);    // Returns the LoginResponseDto with the JWT token and other details
    }
    
    
}