using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repositories;

public class PurchaseRepository: EfRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Purchase> GetPurchaseMovie(int UserId, int MovieId)
    {
        var purchaseMovie =
            await _dbContext.Purchases.Include(g => g.Movie)
                .FirstOrDefaultAsync(p => p.UserId == UserId && p.MovieId == MovieId);
        return purchaseMovie;
    }

    public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id)
    {
        var getPurchase = await _dbContext.Purchases.Include(g => g.Movie)
            .Where(g => g.UserId == id).ToListAsync();
        return getPurchase;
    }
}