using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.User.Interfaces
{
    public interface IRoleRepository : IRepository<ApplicationRole>
    {
        Task<IdentityResult> CreateRoleAsync(ApplicationRole role);
        Task AddRoleAsync(ApplicationRole role);
        Task<ApplicationRole> GetRoleByName(string name);
    }
}
