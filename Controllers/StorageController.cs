using Microsoft.AspNetCore.Mvc;
using Services;

namespace BackendRazor.Controllers
{
  [Route("api/storage")]
  public class StorageController : Controller
  {
    private readonly IStorageService RegistrationService;
    private readonly IAuthService authService;

    public StorageController(IStorageService registrationService, IAuthService authService)
    {
      RegistrationService = registrationService;
      this.authService = authService;
    }

    [HttpPost]
    public IActionResult Register([FromBody] Person person)
    {
      if (!authService.IsLoggedIn())
      {
        return Unauthorized(new { message = "Unauthorized" });
      }
      RegistrationService.Register(person);
      return Ok();
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
      if (!authService.IsLoggedIn())
      {
        return Unauthorized(new { message = "Unauthorized" });
      }
      var people = RegistrationService.GetAll();
      return Ok(people);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
      if (!authService.IsLoggedIn())
      {
        return Unauthorized(new { message = "Unauthorized" });
      }
      RegistrationService.Delete(id);
      return Ok();
    }
  }
}