using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(ShoppingDbContext shoppingDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _shoppingDbContext = shoppingDbContext;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task AddUserToRole(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserAsync(string email)
        {
            User? user = await _shoppingDbContext.Users
             .Include(u => u.City)
             .FirstOrDefaultAsync(u => u.Email == email);
            return user!;

        }

        public async Task<bool> AddUserToRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
