using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Exeptions
{
    public class InvalidPasswordException : AuthorisationExeption
    {
        public InvalidPasswordException() : base("Invalid password") { }

        public InvalidPasswordException(string? message) : base(message)
        {
        }

        public InvalidPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
