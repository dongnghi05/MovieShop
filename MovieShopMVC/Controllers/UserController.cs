using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MovieShopMVC.Controllers;

[Authorize]
public class UserController : Controller
{
    //check if user loged in
    //check user id
    //cookie based authentication
    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Reviews()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        return View();
    }
}