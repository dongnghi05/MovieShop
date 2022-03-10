using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    //Overload
    public async Task<IActionResult> Register(RegisterModel model)
    {
        //save the pw and account info with salt
        var user = await _accountService.CreateUser(model);
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var userLogedIn = await _accountService.ValidateUser(model.Email, model.Password);
        if (userLogedIn != null)
        {
            //create an authentication and store some claims infomation in the cookie
            //user related information
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userLogedIn.Email),
                new Claim(ClaimTypes.NameIdentifier, userLogedIn.Id.ToString()),
                new Claim(ClaimTypes.GivenName, userLogedIn.FirstName),
                new Claim(ClaimTypes.Surname, userLogedIn.LastName),
                new Claim(ClaimTypes.DateOfBirth, userLogedIn.DateOfBirth.ToShortDateString()),

            };
            return LocalRedirect("~/");
        }
        else
        {
            return View();
        }
        
    }
 
}