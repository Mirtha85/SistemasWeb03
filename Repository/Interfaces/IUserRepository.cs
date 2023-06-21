using Microsoft.AspNetCore.Identity;
using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName); //check if a role exists, if not, create the role

        Task AddUserToRole(User user, string roleName); //assign role to user

        Task<bool> AddUserToRoleAsync(User user, string roleName); //check user type

    }
}
