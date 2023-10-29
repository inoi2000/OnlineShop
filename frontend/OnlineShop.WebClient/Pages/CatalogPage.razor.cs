using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShop.HttpModels.Models;
using OnlineShopHttpApiClient;
using System;
using System.Runtime.CompilerServices;

namespace OnlineShop.WebClient.Pages;

public partial class CatalogPage : IDisposable
{
    //private ILocalStorageService localStorage;
    [Inject] private NavigationManager Navigation { get; set; }

    private IReadOnlyList<ProductResponse>? Products { get; set; }

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

    public async Task AddToBasket(ProductResponse product)
    {
        var tempProductList = await LocalStorage.GetItemAsync<List<ProductResponse>>("onlineShop_basket");
        if(tempProductList == null) { tempProductList = new List<ProductResponse>(); }
        var ProductList = new List<ProductResponse>(tempProductList) { product };

        await LocalStorage.SetItemAsync<List<ProductResponse>>("onlineShop_basket", ProductList);
    }

    public async Task AddToCart(ProductResponse product)
    {
        await OnlineShopClient.AddProductToCartAsync(product.Id, _cts.Token);

        //TODO добавить snak при успехе и при ошибке
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Products = await OnlineShopClient.GetAllProductsAsync(_cts.Token);
        MaxPrice = Products.Max(x => x.Price);
        MinPrice = Products.Min(x => x.Price);
    }

    
}
