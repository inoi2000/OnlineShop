using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.HttpModels.Requests
{
    public class RegisterRequest
    {
        public RegisterRequest() { }
        public RegisterRequest(string login, string email, string password, string confirmedPassword) 
        {
            Login = login;
            Email = email;
            Password = password;
            ConfirmedPassword = confirmedPassword;
        }

        [Required, StringLength(8, ErrorMessage = "Логин не может быть более 8 символов"), JsonPropertyName("login")]
        public string Login { get; set; }

        [Required, EmailAddress(ErrorMessage = "Не верно введен Email"), JsonPropertyName("email")]
        public string Email { get; set; }

        [Required, StringLength(30, ErrorMessage = "Паполь должен быть не менее 8 символов", MinimumLength = 8), JsonPropertyName("password")]
        public string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmedPassword { get; set; }
    }
}
