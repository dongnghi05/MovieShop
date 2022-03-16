
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
    Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
    Task<List<PurchaseModel>> GetAllPurchasesForUser(int id);
    Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId);
    Task<int> AddFavorite(FavoriteRequestModel favoriteRequest);
    Task<string> RemoveFavorite(FavoriteRequestModel favoriteRequest);
    Task<bool> FavoriteExists(int id, int movieId);
    Task<List<FavoriteModel>> GetAllFavoritesForUser(int id);
    Task<string> AddMovieReview(ReviewRequestModel reviewRequest);
    Task<string> UpdateMovieReview(ReviewRequestModel reviewRequest);
    Task<string> DeleteMovieReview(int userId, int movieId);
    Task<List<ReviewRequestModel>> GetAllReviewsByUser(int id);
   
}