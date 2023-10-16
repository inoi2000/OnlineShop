using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShopHttpApiClient.Models;

namespace OnlineShop.WebClient.Pages;

public partial class ProductInfoPage
{
    [Parameter] 
    public Guid Id { get; set; }

    [Inject] 
    private IOnlineShopClient OnlineShopClient { get; set; }

    private Product Product { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override async Task OnInitializedAsync()
    {
        Product = await OnlineShopClient.GetProductAsync(Id, _cts.Token);
    }
}
