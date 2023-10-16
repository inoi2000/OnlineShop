using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.HttpModels.Requests
{
    public class AuthorisationRequest
    {
        public AuthorisationRequest() { }
        public AuthorisationRequest(string login, string password) 
        {
            Login = login;
            Password = password;
        }

        [Required(ErrorMessage = "Введите Логин"), JsonPropertyName("login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите Пароль"), JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
