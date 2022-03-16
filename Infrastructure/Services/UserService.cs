using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IReviewRepository _reviewRepository;
    public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
    {
        _userRepository = userRepository;
        _purchaseRepository = purchaseRepository;
        _favoriteRepository = favoriteRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        var purchase = await _purchaseRepository.GetPurchaseMovie(userId, purchaseRequest.MovieId);
        if (purchase != null)
        {
            throw new Exception("Purchase Already existed");
        }

        var newPurchase = new Purchase()
        {
            UserId = userId,
            PurchaseDateTime = purchaseRequest.PurchaseDateTime,
            MovieId = purchaseRequest.MovieId,
            TotalPrice = purchaseRequest.TotalPrice,
            PurchaseNumber = purchaseRequest.PurchaseNumber
        };
        var createPurchase = await _purchaseRepository.Add(newPurchase);
        return createPurchase.Id;
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        var purchase = await _purchaseRepository.GetPurchaseMovie(userId, purchaseRequest.MovieId);
        if (purchase != null)
        {
            return true;
        }
        return false;
    }

    public async Task<List<PurchaseModel>> GetAllPurchasesForUser(int id)
    {
        var purchaseMovie = await _purchaseRepository.GetAllPurchasesForUser(id);
        
        var purchaseList = new List<PurchaseModel>();
        foreach (var purchase in purchaseMovie)
        {
            purchaseList.Add(new PurchaseModel
            {
                PurchaseNumber = purchase.PurchaseNumber,
                TotalPrice = purchase.TotalPrice,
                PurchaseDateTime = purchase.PurchaseDateTime,
                Movie = new MovieCardModel
                {
                    Id = purchase.MovieId,
                    PosterUrl = purchase.Movie.PosterUrl,
                    Title = purchase.Movie.Title
                }
            });
        }

        return purchaseList;
    }

    public async Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId)
    {
        var purchase = await _purchaseRepository.GetPurchaseMovie(userId, movieId);
        if (purchase == null)
        {
            return null;
        }

        var purchaseDetails = new PurchaseRequestModel
        {
            MovieId = movieId,
            PurchaseNumber = purchase.PurchaseNumber,
            TotalPrice = purchase.TotalPrice,
            PurchaseDateTime = purchase.PurchaseDateTime,
            Movie = new MovieCardModel
            {
                Id = movieId,
                PosterUrl = purchase.Movie.PosterUrl,
                Title = purchase.Movie.Title
            }
        };

        return purchaseDetails;
    }

    public async Task<int> AddFavorite(FavoriteRequestModel favoriteRequest)
    {
        var favorite = await _favoriteRepository.GetFavoriteMovie(favoriteRequest.UserId, favoriteRequest.MovieId);
        if (favorite != null)
        {
            throw new Exception("Already add to favorite");
        }

        var newFavorite = new Favorite
        {
            MovieId = favoriteRequest.MovieId,
            UserId = favoriteRequest.UserId
        };

        var createFavorite =  await _favoriteRepository.Add(newFavorite);
        return createFavorite.Id;
    }

    public async Task<string> RemoveFavorite(FavoriteRequestModel favoriteRequest)
    {
        var favorite = await _favoriteRepository.GetFavoriteMovie(favoriteRequest.UserId, favoriteRequest.MovieId);
        if (favorite == null)
        {
            throw new Exception("Have not added to favorite");
        }

        var removeFavorite = await _favoriteRepository.Delete(favorite);
        return "Removed";
    }

    public async Task<bool> FavoriteExists(int userId, int movieId)
    {
        var favorite = await _favoriteRepository.GetFavoriteMovie(userId, movieId);
        if (favorite != null)
        {
            return true;
        }
        return false;
    }

    public async Task<List<FavoriteModel>> GetAllFavoritesForUser(int id)
    {
        var favoriteMovie = await _favoriteRepository.GetAllFavoriteForUser(id);
        var favoriteList = new List<FavoriteModel>();
        foreach (var favorite in favoriteMovie)
        {
            favoriteList.Add(new FavoriteModel()
            {
                UserId = favorite.UserId,
                MovieId = favorite.MovieId,
                Movie = new MovieCardModel
                {
                    Id = id,
                    PosterUrl = favorite.Movie.PosterUrl,
                    Title = favorite.Movie.Title
                }
            });
        }
        return favoriteList;
    }

    public async Task<string> AddMovieReview(ReviewRequestModel reviewRequest)
    {
        var newReview = new Review
        {
            MovieId = reviewRequest.MovieId,
            UserId = reviewRequest.UserId,
            Rating = reviewRequest.Rating,
            ReviewText = reviewRequest.ReviewText
        };
        var createReview =  await _reviewRepository.Add(newReview);
        return "Review had been added.";
    }

    public async Task<string> UpdateMovieReview(ReviewRequestModel reviewRequest)
    {
        var reviewMovie = await _reviewRepository.GetReviewMovie(reviewRequest.UserId, reviewRequest.MovieId);
        if (reviewMovie == null)
        {
            throw new Exception("No review");
        }
        
        return "Review had been updated.";
    }

    public async Task<string> DeleteMovieReview(int userId, int movieId)
    {
        var reviewMovie = await _reviewRepository.GetReviewMovie(userId, movieId);
        if (reviewMovie == null)
        {
            throw new Exception("No review");
        }

        var deleteReview = await _reviewRepository.Delete(reviewMovie);
        return "Review had been deleted";
    }

    public async Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id)
    {
        var reviewMovie = await _reviewRepository.GetAllReviewsByUser(id);
        var reviewList = new List<ReviewRequestModel>();
        foreach (var review in reviewMovie)
        {
            reviewList.Add( new ReviewRequestModel
            {
                ReviewText = review.ReviewText,
                Rating = review.Rating
            });
        }
        return reviewList;
    }

   
}
