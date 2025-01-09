using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace BackendRazor.Controllers;



public class HomeController : Controller
{
    public HomeController()
    {

    }

    //This is our controller for the homepage which ansder the get request from 
    //the home page. And it returns the corresponding view.

    [HttpGet("/")]
    public IActionResult Home()
    {
        return View();

    }

}

