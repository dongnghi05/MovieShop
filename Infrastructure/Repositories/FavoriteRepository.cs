using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Favorite> GetFavoriteMovie(int UserId, int MovieId)
    {
        var favoriteMovie =
            await _dbContext.Favorites.Include(f=>f.Movie)
                .FirstOrDefaultAsync(f => f.UserId == UserId && f.MovieId == MovieId);
        return favoriteMovie;
    }

    public async Task<IEnumerable<Favorite>> GetAllFavoriteForUser(int id)
    {
        var getFavorite = await _dbContext.Favorites.Include(g => g.Movie)
            .Where(g => g.UserId == id).ToListAsync();
        return getFavorite;
    }
}