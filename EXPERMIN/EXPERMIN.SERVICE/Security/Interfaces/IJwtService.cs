using EXPERMIN.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Security.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
        Task<bool> RevokedToken(string token);
        Task<bool> IsTokenRevoked(string token);
    }
}
