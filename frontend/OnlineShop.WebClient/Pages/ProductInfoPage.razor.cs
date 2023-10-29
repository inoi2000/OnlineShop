using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShop.HttpModels.Models;

namespace OnlineShop.WebClient.Pages;

public partial class ProductInfoPage
{
    [Parameter] public Guid Id { get; set; }

    private ProductResponse Product { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Product = await OnlineShopClient.GetProductAsync(Id, _cts.Token);
    }
}
