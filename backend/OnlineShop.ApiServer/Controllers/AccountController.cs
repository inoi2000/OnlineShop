using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exeptions;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;

namespace OnlineShop.ApiServer.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController([FromServices] AccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("registration")]
        public async Task<ActionResult<Account>> RegisterAccount(RegisterRequest request, CancellationToken token)
        {
            try
            {
                await _accountService.Register(request.Login, request.Email, request.Password, token);
                return Ok();
            }
            catch (EmailAlreadyExistsExeption ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
        }

        [HttpPost("authorisation")]
        public async Task<ActionResult<AuthorisationResponse>> AuthorisationAccount(AuthorisationRequest request, CancellationToken token)
        {
            try
            {
                var account = await _accountService.Authorisation(request.Login, request.Password, token); 
                return new AuthorisationResponse(account.Id, account.Login);
            }
            catch (AccountNotFoundExeption ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
            catch (InvalidPasswordException ex)
            {
                return Conflict(new ErrorResponse(ex.Message, System.Net.HttpStatusCode.Conflict));
            }
        }

    }
}
