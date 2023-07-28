using AirMastera.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Identity.Controllers;

public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel
        {
            ReturnUrl = returnUrl
        };
        return Ok(viewModel);
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        return Ok(loginViewModel);
    }
}