using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Entities
{
    public class Account : IEntity
    {
        private readonly Guid _id;
        private string _login;
        private string _email;
        private string _passwordHash;

        public Account(Guid id, string login, string email, string passwordHash)
        {
            _id = id;
            _login = login;
            _email = email;
            _passwordHash = passwordHash;
        }

        public Account(string login, string email, string passwordHash)
        {
            _id = Guid.NewGuid();
            _login = login;
            _email = email;
            _passwordHash = passwordHash;
        }

        public Guid Id { get => _id; init => _id = value; }
        public string Login 
        { 
            get => _login; 
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _login = value; 
            } 
        }
        public string Email 
        { 
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                if (new EmailAddressAttribute().IsValid(value))
                {
                    throw new ArgumentException("Value is not a valid email address", nameof(value));
                }
                _email = value;
            }
                 
        }
        public string PasswordHash
        {
            get => _passwordHash;
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _passwordHash = value; 
            }
        }
    }
}
