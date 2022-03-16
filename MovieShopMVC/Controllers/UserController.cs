using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;
    public UserController(ICurrentUser currentUser, IUserService userService)
    {
        _currentUser = currentUser;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Purchases()
    {
        var userId = _currentUser.UserId;
        var purchases = await _userService.GetAllPurchasesForUser(userId);
        return View(purchases);
    }

    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        var userId = _currentUser.UserId;
        var favorites = await _userService.GetAllFavoritesForUser(userId);
        return View(favorites);
    }

    [HttpGet]
    public async Task<IActionResult> Reviews()
    {
        var userId = _currentUser.UserId;
        var review = await _userService.GetAllReviewsByUser(userId);
        return View(review);
    }

    [HttpPost]
    public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchaseRequest)
    {
        var userId = _currentUser.UserId;
        var buyMovie = await _userService.PurchaseMovie(purchaseRequest, userId);
        return Redirect($"~/Movies/Details/{purchaseRequest.MovieId}");
    }

    [HttpPost]
    public async Task<IActionResult> FavoriteMovie(int movieId)
    {
        var userId = _currentUser.UserId;
        var favoriteMovie = await _userService.AddFavorite(new FavoriteRequestModel
        {
            MovieId = movieId,
            UserId = userId
        });
        return Redirect($"~/Movies/Details/{movieId}");
    }
    
    [HttpPost]
    public async Task<IActionResult> UnFavoriteMovie(int movieId)
    {
        var userId = _currentUser.UserId;
        var unFavoriteMovie = await _userService.RemoveFavorite(new FavoriteRequestModel
        {
            MovieId = movieId,
            UserId = userId
        });
        return Redirect($"~/Movies/Details/{movieId}");
    }

    [HttpPost]
    public async Task<IActionResult> ReviewMovie(ReviewRequestModel reviewRequest)
    {
        var userId = _currentUser.UserId;
        var reviewMovie = await _userService.AddMovieReview(reviewRequest);
        return Redirect($"~/Movies/Details/{reviewRequest.MovieId}");
    }
}