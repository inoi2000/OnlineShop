using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPasswordHasherLib
{
    public class IdentityPasswordHasher : IApplicationPasswordHasher
    {
        private readonly PasswordHasher<object> _passwordHasher = new();
        private readonly object _fake = new();
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(_fake, password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword, out bool rehashNeeded)
        {
            var result = _passwordHasher.VerifyHashedPassword(_fake, hashedPassword, providedPassword);
            rehashNeeded = result == PasswordVerificationResult.SuccessRehashNeeded;
            return result != PasswordVerificationResult.Failed;
        }
    }
}
