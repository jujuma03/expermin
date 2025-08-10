using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.User.Implementations
{

    public class RoleRepository : Repository<ApplicationRole>, IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(
            ExperminContext context,
            RoleManager<ApplicationRole> roleManager
            ) : base(context)
        {
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateRoleAsync(ApplicationRole role)
            => await _roleManager.CreateAsync(role);
        public async Task AddRoleAsync(ApplicationRole role)
            => await _context.Roles.AddAsync(role);
        public async Task<ApplicationRole> GetRoleByName(string name)
            => await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}
