using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AirMastera.Infrastructure.Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AirMastera.Infrastructure.Identity.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _interactionService = interactionService ?? throw new ArgumentNullException(nameof(interactionService));
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel
        {
            ReturnUrl = "http://localhost:5000/index.html"
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        var user = await _userManager.FindByNameAsync(loginViewModel.Username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(loginViewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
        if (result.Succeeded)
        {
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        ModelState.AddModelError(string.Empty, "Login error");
        return View(loginViewModel);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var viewModel = new RegisterViewModel
        {
            ReturnUrl = "http://localhost:5000/index.html"
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new AppUser
        {
            UserName = viewModel.Username,
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Redirect("http://localhost:5000/index.html");
        }

        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect("http://localhost:5000/index.html");
    }

    private string GenerateJwtToken(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHere!"); // Задай длинный секретный ключ
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}