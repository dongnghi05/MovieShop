using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string email);
}