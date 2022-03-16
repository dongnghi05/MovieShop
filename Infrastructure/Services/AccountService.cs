using System.Security.Cryptography;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation; 

namespace Infrastructure.Services;

public class AccountService: IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponseModel> ValidateUser(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email); 
        if (user == null)
        {
            throw new Exception("un/pw in valid");
        }

        var hashedPassword = GetHashedPassword(password, user.Salt);
        if (hashedPassword == user.HashedPassword )
        {
            return new LoginResponseModel
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
        }

        return null;
    }
    public async Task<int> CreateUser(RegisterModel model)
    {
        //check if user has registered with the same email
        //go to user repo and get user record from user table by email
        var dbUser = await _userRepository.GetUserByEmail(model.Email);

        if (dbUser != null)
        {
            throw new Exception("Email exists");
        }
        
        //continue to register
        //generate a random salt
        //hash the pw with salt

        var salt = GetRandomSalt();
        var hasedPassword = GetHashedPassword(model.Password, salt);

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Salt = salt,
            HashedPassword = hasedPassword,
            DateOfBirth = model.DateOfBirth
        };
        
        //save the user to User Table
        var createUser =  await _userRepository.Add(user);
        return createUser.Id;
    }

    private string GetRandomSalt()
    {
        var randomBytes = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    private string GetHashedPassword(string password, string salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
            Convert.FromBase64String(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8));
        return hashed;
    }
}