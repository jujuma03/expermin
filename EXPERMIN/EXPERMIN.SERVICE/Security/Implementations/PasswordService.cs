using EXPERMIN.SERVICE.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Security.Implementations
{

    public class PasswordService : IPasswordService
    {
        public PasswordService()
        {

        }
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
