using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotnetPhonebook.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DotnetPhonebook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
        public IActionResult Index(string username,string password)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", "Home");
            }
            ClaimsIdentity? identity = null;
            bool isAuthenticate = false;
            if (username=="admin" && password=="1234qwer")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
            }
            if (username == "User" && password == "user1234")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
            }
            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Phones");
            }
            return View();
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
