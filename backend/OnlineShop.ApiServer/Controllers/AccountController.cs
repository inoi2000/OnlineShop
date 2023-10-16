using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exeptions;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;
using System.Security.Claims;

namespace OnlineShop.ApiServer.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ITokenService _tokenService;
        public AccountController([FromServices] AccountService accountService, [FromServices] ITokenService tokenService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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
                var accountToken = _tokenService.GenerateToken(account);
                return new AuthorisationResponse(account.Id, account.Login, accountToken);
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

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<AccountResponse>> GetCurrentAccount(CancellationToken cancellationToken)
        {
            var strId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var guid = Guid.Parse(strId);
            var account = await _accountService.GetAccountById(guid, cancellationToken);
            return new AccountResponse(account.Id, account.Login, account.Email);
        }

    }
}
