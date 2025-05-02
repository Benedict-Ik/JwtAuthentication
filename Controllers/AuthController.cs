using JwtAuthentication.Models;
using JwtAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    // Specifies that this controller responds to routes like "api/AuthController/ActionName"
    [Route("api/[controller]/[action]")]
    // Enables automatic model validation and binding for API controllers
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Service responsible for generating JWT tokens
        private readonly JwtService _jwtService;
        // Stores admin credentials loaded from appsettings.json
        private readonly UserLogin _userLoginCred;

        // Constructor: injects JwtService and IConfiguration to access appsettings.json
        public AuthController(JwtService jwtService, IConfiguration configuration)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            // Bind the "AdminCredentials" section from appsettings.json to a UserLogin object
            _userLoginCred = configuration.GetSection("AdminCredentials").Get<UserLogin>();
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            //if (userLogin.Username == "username" && userLogin.Password == "password")
            if (userLogin.Username == _userLoginCred.Username &&
                userLogin.Password == _userLoginCred.Password)
            {
                var token = _jwtService.GenerateToken(userLogin.Username);
                return Ok(new
                {
                    message = "Authenticated successfully",
                    token = token
                });
            }
            return Unauthorized();
        }

        // Protected endpoint: accessible only to authenticated users
        [Authorize]
        [HttpGet]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure data by an authenticated user.");
        }

    }
}
