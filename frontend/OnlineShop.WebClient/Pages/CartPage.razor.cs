using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShop.HttpModels.Models;
using OnlineShopHttpApiClient;

namespace OnlineShop.WebClient.Pages;

public partial class CartPage : IDisposable
{
    [Inject] private NavigationManager Navigation { get; set; }

    private List<ProductResponse>? Products { get; set; }
    private CartResponse Cart { get; set; }

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
            var cart = await OnlineShopClient.GetCartAsync(_cts.Token);
            //TODO добавить функционал удаления

            //await LocalStorage.SetItemAsync<List<Product>>("onlineShop_basket", Products!);
        }
        await OnInitializedAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Cart = await OnlineShopClient.GetCartAsync(_cts.Token);
        //Products = await OnlineShopClient.GetProductsFromCartAsync(_cts.Token);
        if (Cart == null) { Cart = new CartResponse(); }
    }

    
}
