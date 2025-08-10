using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Jwt
{
    public interface IJwtRepository : IRepository<RevokedToken>
    {
        Task<bool> IsTokenRevoked(string token);
    }
}
