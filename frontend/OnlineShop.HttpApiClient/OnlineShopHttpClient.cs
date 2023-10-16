using OnlineShop.HttpApiClient;
using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;
using OnlineShopHttpApiClient.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OnlineShopHttpApiClient
{
    public class OnlineShopHttpClient : IDisposable, IOnlineShopClient
    {
        private readonly string _host;
        private readonly HttpClient _httpClient;
        public OnlineShopHttpClient(string host = "http://myshop.com/", HttpClient? httpClient = null)
        {
            if (host is null) {  throw new ArgumentNullException(nameof(host)); }

            if (!Uri.TryCreate(host, UriKind.Absolute, out var hostUri))
            {
                throw new ArgumentException("The host adress should be a valif url", nameof(host));
            }
            _host = host;
            _httpClient = httpClient ?? new HttpClient();
            if (_httpClient.BaseAddress is null)
            {
                _httpClient.BaseAddress = hostUri;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        #region ProductClient
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken token)
        {
            var products = _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>("catalog/get_all_products", token);
            if (products is null)
            {
                throw new InvalidOperationException("The server returned null product");
            }
            var product = await products;
            if (product is null) 
            { 
                throw new InvalidOperationException("The server returned null product"); 
            }
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(decimal minPrice, decimal maxPrice, CancellationToken token)
        {
            var products = _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>($"catalog/get_products?minPrice={minPrice}&maxPrice={maxPrice}", token);
            if (products is null)
            {
                throw new InvalidOperationException("The server returned null product");
            }
            var product = await products;
            if (product is null)
            {
                throw new InvalidOperationException("The server returned null product");
            }
            return product;
        }

        public async Task<Product> GetProductAsync(Guid id, CancellationToken token)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"catalog/get_product?Id={id}", token);
            if (product is null) { throw new InvalidOperationException("The server returned null product"); }
            return product;
        }

        public async Task AddProductAsync(Product product, CancellationToken token)
        {
            using var response = await _httpClient.PostAsJsonAsync("catalog/add_product", product, token);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditProductAsync(Product product, CancellationToken token)
        {
            using var response = await _httpClient.PutAsJsonAsync("catalog/edit_product", product, token);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken token)
        {
            using var response = await _httpClient.DeleteAsync($"catalog/delete_product?Id={id}", token);
            response.EnsureSuccessStatusCode();
        }

        #endregion

        #region AccountClient
        public async Task RegisterAccountAsync(RegisterRequest request, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            using var response = await _httpClient.PostAsJsonAsync("accounts/registration", request, token);
            
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    throw new OnlineShopApiExeption(error!);
                } else if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var details = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                    throw new OnlineShopApiExeption(response.StatusCode, details!);
                }
                else
                {
                    throw new OnlineShopApiExeption("Неизвесная ошибка!");
                }
            }
        }


        public async Task<AuthorisationResponse> AuthorisationAsync(AuthorisationRequest request, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            using var response = await _httpClient.PostAsJsonAsync("accounts/authorisation", request, token);

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                    {
                        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                        throw new OnlineShopApiExeption(error!);
                    }
                    case HttpStatusCode.BadRequest:
                    {
                        var details = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                        throw new OnlineShopApiExeption(response.StatusCode, details!);
                    }
                    default:
                        throw new OnlineShopApiExeption("Неизвесная ошибка!");
                }
            }

            var authorisationResponse = await response.Content.ReadFromJsonAsync<AuthorisationResponse>(cancellationToken: token);

            var headerValue = new AuthenticationHeaderValue("Bearer", authorisationResponse?.Token);
            _httpClient.DefaultRequestHeaders.Authorization = headerValue;

            return authorisationResponse!;
        }
        #endregion
    }
}
