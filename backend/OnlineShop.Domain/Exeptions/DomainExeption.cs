using System.Runtime.Serialization;

namespace OnlineShop.Domain.Exeptions
{
    [Serializable]
    public class DomainExeption : Exception
    {
        public DomainExeption()
        {
        }

        public DomainExeption(string? message) : base(message)
        {
        }

        public DomainExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DomainExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}