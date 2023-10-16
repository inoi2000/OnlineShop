using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShopHttpApiClient.Models;

namespace OnlineShop.WebClient.Pages;

public partial class EditProductPage
{
    [Parameter] public Guid Id { get; set; }

    [Inject] private IOnlineShopClient OnlineShopClient { get; set; }

    private string Name { get; set; } = String.Empty;
    private string Description { get; set; } = String.Empty;
    private string ImagUri { get; set; } = String.Empty;
    private decimal Price { get; set; }

    MudForm form;


    private Product Product { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public async Task SaveChanges()
    {
        var newProduct = new Product(Product.Id, Name, Description, Price, new Uri(ImagUri));
        await OnlineShopClient.EditProductAsync(newProduct, _cts.Token);
    }


    protected override async Task OnInitializedAsync()
    {
        Product = await OnlineShopClient.GetProductAsync(Id, _cts.Token);

        Name = Product.Name;
        Description = Product.Description;
        ImagUri = Product.ImagUri.ToString(); 
        Price = Product.Price;
    }
}
