using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<Review> GetReviewMovie(int UserId, int MovieId);
    Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
}