using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Exeptions
{
    public class AccountNotFoundExeption : AuthorisationExeption
    {
        public AccountNotFoundExeption() : base("Account with given login not found") { }

        public AccountNotFoundExeption(string? message) : base(message)
        {
        }

        public AccountNotFoundExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AccountNotFoundExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
