using Microsoft.AspNetCore.Mvc;
using Services;

namespace frontend.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (authService.Login(request.Username, request.Password))
            {
                return Ok(new { message = "Login successful" });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            var isLoggedIn = authService.IsLoggedIn();
            var username = authService.GetLoggedInUser();
            return Ok(new { isLoggedIn, username });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
