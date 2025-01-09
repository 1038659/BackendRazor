using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Services;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IHostEnvironment _env;

        private ISession Session => HttpContext.Session;

        public AuthController(IAuthService authService, IHostEnvironment env)
        {
            this.authService = authService;
            this._env = env;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (authService.Login(request.Username, request.Password))
            {
                // Create session
                HttpContext.Session.SetString("Username", request.Username);
                HttpContext.Session.SetString("IsLoggedIn", "true");

                

                // Access a file from "ExternalFiles"
                var filePath = Path.Combine(_env.ContentRootPath, "ExternalFiles", "welcome.txt");
                if (System.IO.File.Exists(filePath))
                {
                    var welcomeMessage = System.IO.File.ReadAllText(filePath);
                    return Ok(new { message = "Login successful", welcomeMessage });
                }

                return Ok(new { message = "Login successful" });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            var isLoggedIn = authService.IsLoggedIn();
            var username = authService.GetLoggedInUser();
            return Ok(new { isLoggedIn, username});
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();
            return Ok(new { message = "Logout successful" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
