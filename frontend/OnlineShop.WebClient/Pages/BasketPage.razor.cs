using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShopHttpApiClient;
using OnlineShopHttpApiClient.Models;

namespace OnlineShop.WebClient.Pages;

public partial class BasketPage : IDisposable
{
    [Inject] private NavigationManager Navigation { get; set; }

    private List<Product>? Products { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public void Dispose()
    {
        _cts.Cancel();
    }

    public void NavigateToProductInfo(Guid id)
    {
        Navigation.NavigateTo($"/product_info/{id}", false);
    }

    public async Task RemoveFromBasket(Product product)
    {
        if (Products != null)
        {
            Products?.Remove(product);
            await LocalStorage.SetItemAsync<List<Product>>("onlineShop_basket", Products!);
        }
        await OnInitializedAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        Products = await LocalStorage.GetItemAsync<List<Product>>("onlineShop_basket");
        if (Products == null) { Products = new List<Product>(); }
    }

    
}
