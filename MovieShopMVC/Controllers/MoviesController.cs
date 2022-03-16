using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        
        public MoviesController(IMovieService movieService, ICurrentUser currentUser, IUserService userService)
        {
            _movieService = movieService;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<IActionResult> Details(int id)
        {
            // Movie Service with Details
            // pass the movie details data to view
            // Data
            // Remote Database 

            // CPU bound operation => PI => Loan callcuator, image pro
            // I/O bound operation => database calls, file, images, videos

            // Network speed, SQL Server => Query , Server Memory
            // T1 is just waiting
            ViewBag.IsPurchased = false;
            ViewBag.IsFavorited = false;
            if (_currentUser.IsAuthenticated)
            {
                var currentUser = _currentUser.UserId;
                var isPurchase = await _userService.GetPurchasesDetails(currentUser, id);
                if (isPurchase != null)
                {
                    ViewBag.IsPurchased = true;
                }
                var isFavorite = await _userService.FavoriteExists(currentUser, id);
                if (isFavorite)
                {
                    ViewBag.IsFavorited = true;
                }
                
            }
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            return View("PagedMovies", pagedMovies );
        }
    }
}