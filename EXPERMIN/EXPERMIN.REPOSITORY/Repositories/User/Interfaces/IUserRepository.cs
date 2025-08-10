using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.User.Interfaces
{

    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmailAsync(string value);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user);
        Task<ApplicationUser> FindByNameAsync(string name);
        Task<bool> AnyByUsername(string username);
        Task<bool> AnyByEmail(string email);
        Task<bool> ExistsAsync(string userId);
        Task AddToRoleAsync(ApplicationUser user, string role);
        Task UpdateToRoleAsync(ApplicationUser user, string role);
        Task<List<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser> GetUserById(string id);
        Task<List<ApplicationRole>> GetRolesAsync(string userId);
        PasswordOptions GetPasswordOptions();
    }
}
