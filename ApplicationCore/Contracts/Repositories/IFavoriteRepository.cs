using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IFavoriteRepository : IRepository<Favorite> 
{
    Task<Favorite> GetFavoriteMovie(int UserId, int MovieId);
    Task<IEnumerable<Favorite>> GetAllFavoriteForUser(int id);
}