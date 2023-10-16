using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShopHttpApiClient;
using OnlineShopHttpApiClient.Models;
using System;
using System.Runtime.CompilerServices;

namespace OnlineShop.WebClient.Pages;

public partial class CatalogPage : IDisposable
{
    //private ILocalStorageService localStorage;
    [Inject] private IOnlineShopClient OnlineShopClient { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }

    private IReadOnlyList<Product>? Products { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    private decimal MinPrice { get; set; }
    private decimal MaxPrice { get; set; }

    public void Dispose()
    {
        _cts.Cancel();
    }

    public async Task StartSearch() 
    {
        Products = await OnlineShopClient.GetProductsAsync(MinPrice, MaxPrice, _cts.Token);
    }

    public void NavigateToProductInfo(Guid id)
    {
        Navigation.NavigateTo($"/product_info/{id}", false);
    }

    public async Task AddToBasket(Product product)
    {
        var tempProductList = await localStorage.GetItemAsync<List<Product>>("onlineShop_basket");
        if(tempProductList == null) { tempProductList = new List<Product>(); }
        var ProductList = new List<Product>(tempProductList) { product };

        await localStorage.SetItemAsync<List<Product>>("onlineShop_basket", ProductList);
    }

    protected override async Task OnInitializedAsync()
    {
        Products = await OnlineShopClient.GetAllProductsAsync(_cts.Token);
        MaxPrice = Products.Max(x => x.Price);
        MinPrice = Products.Min(x => x.Price);
    }

    
}
