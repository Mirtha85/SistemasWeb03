using Microsoft.AspNetCore.Identity;
using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers { get; }
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<User> AddUserAsync(AddUserViewModel model);

        Task CheckRoleAsync(string roleName); //check if a role exists, if not, create the role

        Task AddUserToRole(User user, string roleName); //assign role to user

        Task<bool> AddUserToRoleAsync(User user, string roleName); //check user type

        Task<SignInResult> LoginAsync(LoginViewModel model); //devuelve un signInResult, obj que dice si pudo o no logearse

        Task LogoutAsync();


    }
}
