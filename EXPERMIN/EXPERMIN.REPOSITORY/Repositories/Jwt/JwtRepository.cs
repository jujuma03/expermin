using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Jwt
{
    public class JwtRepository : Repository<RevokedToken>, IJwtRepository
    {
        public JwtRepository(ExperminContext context) : base(context)
        {
        }
        public async Task<bool> IsTokenRevoked(string token)
            => await _context.RevokedTokens.AnyAsync(rt => rt.Token == token);
    }
}
