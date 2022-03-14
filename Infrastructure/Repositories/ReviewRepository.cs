using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository : EfRepository<Review>, IReviewRepository
{
    public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Review> GetReviewMovie(int UserId, int MovieId)
    {
        var reviewMovie = 
            await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == UserId && r.MovieId == MovieId);
        return reviewMovie;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
    {
        var getReview =
            await _dbContext.Reviews.Include(g => g.Movie)
                .Where(g => g.UserId == id).ToListAsync();
        return getReview;
    }
} 