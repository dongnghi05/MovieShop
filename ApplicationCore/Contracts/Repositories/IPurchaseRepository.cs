using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository : IRepository<Purchase>
{
    Task<Purchase> GetPurchaseMovie(int UserId, int MovieId);
    Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id);
    
    
}