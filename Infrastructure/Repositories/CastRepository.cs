using ApplicationCore.Contracts.Respositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories;

public class CastRepository: EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Cast> GetById(int id)
    {
        var movieCast = await _dbContext.Casts.Include(c => c.MovieCasts)
            .ThenInclude(c => c.Movie).FirstOrDefaultAsync(c=>c.Id == id);
        return movieCast;
    }
    
}