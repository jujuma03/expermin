﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Security.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
