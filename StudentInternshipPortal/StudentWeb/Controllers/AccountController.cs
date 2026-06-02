using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller manages web user accounts: login, registration, and logout flows.
public class AccountController : Controller
{
    private readonly StudentAuthService _authService;

    public AccountController(StudentAuthService authService)
    {
        _authService = authService;
    }

    // Displays the login page. Redirects to Home if already logged in.
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
        return View(new LoginViewModel());
    }

    // Handles the login form submission. Validates credentials and writes auth cookie.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // Validate credentials through authentication service
        var user = _authService.ValidateStudent(model.Username, model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // Setup user identity claims (ID, name, role)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Sign in using cookie authentication scheme
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Home");
    }

    // Displays the student registration page.
    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
        return View(new RegisterViewModel());
    }

    // Handles the student register form submission.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // Check if username already exists in the system
        if (_authService.IsUsernameTaken(model.Username))
        {
            ModelState.AddModelError("Username", "Username is already taken.");
            return View(model);
        }

        // Register the student and initialize their profile page
        var success = _authService.RegisterStudent(model.Username, model.Password);
        if (success)
        {
            TempData["SuccessMessage"] = "Registration successful! You can now log in.";
            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError(string.Empty, "An error occurred while creating your account. Please try again.");
        return View(model);
    }

    // Handles user logout. Deletes the authentication cookie.
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }
}