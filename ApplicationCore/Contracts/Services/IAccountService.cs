using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    Task<LoginResponseModel>ValidateUser(string email, string password);
    Task<int> CreateUser(RegisterModel model);
}