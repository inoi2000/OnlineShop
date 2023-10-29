using Microsoft.Extensions.Logging;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exeptions;
using OnlineShop.Domain.Interfaces;
using System.Xml.Linq;

namespace OnlineShop.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationPasswordHasher _hasher;        
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            IAccountRepository accountRepository, 
            IApplicationPasswordHasher hasher,
            IUnitOfWork uow,
            ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public virtual async Task Register(string login, string email, string password, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть пустым или содержать только пробел.", nameof(login)); }
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException($"\"{nameof(email)}\" не может быть пустым или содержать только пробел.", nameof(email)); }
            if (string.IsNullOrWhiteSpace(password)) { throw new ArgumentException($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password)); }
                        
            var existedAccount = await _accountRepository.FindAccountByEmail(email, token);
            if (existedAccount is not null)
            {
                throw new EmailAlreadyExistsExeption();
            }
            Account account = new Account(login, email, EncryptPassword(password));
            //await _accountRepository.Add(account, token);
            var cart = new Cart(account.Id);

            await _uow.AccountRepository.AddUnsafe(account, token);
            await _uow.CartRepository.AddUnsafe(cart, token);
            await _uow.SaveChangesAsync(token);
        }


        public virtual async Task<Account> Authorisation(string login, string password, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNullOrEmpty($"\"{nameof(login)}\" не может быть пустым или содержать только пробел.", nameof(login));
            ArgumentNullException.ThrowIfNullOrEmpty($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password));

            var account = await _accountRepository.FindAccountByLogin(login, token);
            if (account is null)
            {
                throw new AccountNotFoundExeption("Account with given login not found");
            } 
            
            var isPasswordValid = _hasher.VerifyHashedPassword(account.PasswordHash, password, out var rehashNeeded);
            if (!isPasswordValid)
            {
                throw new InvalidPasswordException("Invalid password");
            }

            if (rehashNeeded)
            {
                await RehashPassword(password, account, token);
            }

            return account;

        }

        private Task RehashPassword(string password, Account account, CancellationToken token) 
        {
            account.PasswordHash = EncryptPassword(password);
            return _accountRepository.Update(account, token);
        }

        private string EncryptPassword(string password)
        {
            var hashPassword = _hasher.HashPassword(password);
            _logger.LogDebug($"Password hasher {hashPassword}");
            return hashPassword;
        }

        public async Task<Account> GetAccountById(Guid id, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetById(id, cancellationToken);
        }

    }
}