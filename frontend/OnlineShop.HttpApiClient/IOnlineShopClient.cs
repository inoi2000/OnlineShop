using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;
using OnlineShopHttpApiClient.Models;
using System.Net.Http;

namespace OnlineShopHttpApiClient
{
    public interface IOnlineShopClient
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken token);
        Task<IReadOnlyList<Product>> GetProductsAsync(decimal minPrice, decimal maxPrice, CancellationToken token);
        Task<Product> GetProductAsync(Guid id, CancellationToken token);
        Task AddProductAsync(Product product, CancellationToken token);
        Task EditProductAsync(Product product, CancellationToken token);
        Task DeleteProductAsync(Guid id, CancellationToken token);


        Task RegisterAccountAsync(RegisterRequest request, CancellationToken token);
        Task<AuthorisationResponse> AuthorisationAsync(AuthorisationRequest request, CancellationToken token);
        void SetAuthorizationToken(string token);
        void DeleteAuthorizationToken();
        Task<AccountResponse> GetCurrentUser(CancellationToken token);
    }
}