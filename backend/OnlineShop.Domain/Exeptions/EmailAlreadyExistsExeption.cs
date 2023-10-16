using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Exeptions
{
    public class EmailAlreadyExistsExeption : DomainExeption
    {
        public EmailAlreadyExistsExeption() : base("Account with this email already exists") { }

        public EmailAlreadyExistsExeption(string? message) : base(message)
        {
        }

        public EmailAlreadyExistsExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailAlreadyExistsExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
