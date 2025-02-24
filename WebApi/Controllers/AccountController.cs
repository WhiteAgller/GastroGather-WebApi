using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.AuthServer.Models;

namespace WebApi.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private readonly SignInManager<User.Domain.User> _signInManager;
    private readonly UserManager<User.Domain.User> _userManager;

    public AccountController(SignInManager<User.Domain.User> signInManager, UserManager<User.Domain.User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User.Domain.User();
        var userNameAlreadyExists = false;

        var numberOfAttempts = 0;
        while (!userNameAlreadyExists || numberOfAttempts > 10)
        {
            var randomNumber = RandomNumberGenerator.GetInt32(1000, 9999);
            var name = $"{model.Username}#{randomNumber}";
            user = new User.Domain.User { Id = name, UserName = $"{model.Username}", Email = model.Email};
            userNameAlreadyExists = _userManager.FindByNameAsync(user.UserName) != null;
            numberOfAttempts++;
        }
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }
    
    [HttpGet("login")]
    public IActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginModel());
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return Redirect(returnUrl ?? "/");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }
}