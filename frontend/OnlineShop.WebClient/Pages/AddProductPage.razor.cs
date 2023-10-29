using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShop.HttpModels.Models;

namespace OnlineShop.WebClient.Pages;

public partial class AddProductPage
{
    private string Name { get; set; }
    private string Description { get; set; }
    private string ImagUri { get; set; }
    private decimal Price { get; set; }

    MudForm form;

    private CancellationTokenSource _cts = new CancellationTokenSource();

    private async Task SaveProduct()
    {
        var Product = new ProductResponse(Name, Description, Price, new Uri(ImagUri));
        await OnlineShopClient.AddProductAsync(Product, _cts.Token);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}
