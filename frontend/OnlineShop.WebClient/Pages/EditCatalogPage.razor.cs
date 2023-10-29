using Microsoft.AspNetCore.Components;
using OnlineShop.HttpModels.Models;
using OnlineShopHttpApiClient;
using System;
using System.Runtime.CompilerServices;

namespace OnlineShop.WebClient.Pages;

public partial class EditCatalogPage : IDisposable
{
    [Inject] private NavigationManager Navigation { get; set; }

    private IReadOnlyList<ProductResponse>? Products { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public void Dispose()
    {
        _cts.Cancel();
    }

    public void NavigateToProductInfo(Guid id)
    {
        Navigation.NavigateTo($"/product_info/{id}", false);
    }

    public void NavigateToEditProduct(Guid id)
    {
        Navigation.NavigateTo($"/edit_product/{id}", false);
    }

    public async Task DeleatProduct(Guid id)
    {
        await OnlineShopClient.DeleteProductAsync(id, _cts.Token);
        await OnInitializedAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Products = await OnlineShopClient.GetAllProductsAsync(_cts.Token);
    }

    
}
