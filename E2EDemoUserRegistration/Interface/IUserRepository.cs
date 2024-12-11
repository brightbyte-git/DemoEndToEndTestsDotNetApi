using E2EDemoUserRegistration.Model;
using Microsoft.AspNetCore.Mvc;

namespace E2EDemoUserRegistration.Interface;

public interface IUserRepository
{
    Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto);
}