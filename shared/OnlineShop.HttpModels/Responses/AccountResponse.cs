using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.HttpModels.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public AccountResponse(Guid id, string name, string email) 
        {
            Id = id;
            Name = name;
            Email = email;
        }

    }
}
