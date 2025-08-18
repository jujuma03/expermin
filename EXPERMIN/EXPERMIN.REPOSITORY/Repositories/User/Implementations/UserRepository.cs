using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.User.Implementations
{

    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(ExperminContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }
        public PasswordOptions GetPasswordOptions()
            => _userManager.Options.Password;
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var normalizedEmail = email.ToUpper();
            return await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return await _userManager.Users
                            .Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                            .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user)
            => await _userManager.GetUserAsync(user);
        public async Task<ApplicationUser> FindByNameAsync(string name)
            => await _userManager.FindByNameAsync(name);
        public async Task<bool> AnyByUsername(string username)
            => await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        public async Task<bool> AnyByEmail(string email)
            => await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        public async Task<bool> ExistsAsync(string userId)
            => await _context.Users.AnyAsync(x => x.Id == userId);
        public async Task InsertToRoleAsync(ApplicationUser user, string role)
            => await _userManager.AddToRoleAsync(user, role);
        public async Task<List<ApplicationUser>> GetAllUserAsync()
            => await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        public async Task<ApplicationUser> GetUserById(string id)
            => await _context.Users.Where(u => u.Id == id)
                            .Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                            .FirstOrDefaultAsync();
        public async Task<List<ApplicationRole>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<ApplicationRole>();

            var roleNames = await _userManager.GetRolesAsync(user);

            var roles = await _context.Roles
                .Where(r => roleNames.Contains(r.Name))
                .ToListAsync();
            return roles;
        }

    }
}
