using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShop.HttpModels.Models;
using OnlineShopHttpApiClient;

namespace OnlineShop.WebClient.Pages;

public partial class BasketPage : IDisposable
{
    [Inject] private NavigationManager Navigation { get; set; }

    private List<ProductResponse>? Products { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public void Dispose()
    {
        _cts.Cancel();
    }

    public void NavigateToProductInfo(Guid id)
    {
        Navigation.NavigateTo($"/product_info/{id}", false);
    }

    public async Task RemoveFromBasket(ProductResponse product)
    {
        if (Products != null)
        {
            Products?.Remove(product);
            await LocalStorage.SetItemAsync<List<ProductResponse>>("onlineShop_basket", Products!);
        }
        await OnInitializedAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Products = await LocalStorage.GetItemAsync<List<ProductResponse>>("onlineShop_basket");
        if (Products == null) { Products = new List<ProductResponse>(); }
    }

    
}
