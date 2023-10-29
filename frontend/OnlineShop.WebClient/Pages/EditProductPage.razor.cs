using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShop.HttpModels.Models;

namespace OnlineShop.WebClient.Pages;

public partial class EditProductPage
{
    [Parameter] public Guid Id { get; set; }

    private string Name { get; set; } = String.Empty;
    private string Description { get; set; } = String.Empty;
    private string ImagUri { get; set; } = String.Empty;
    private decimal Price { get; set; }

    MudForm form;


    private ProductResponse Product { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public async Task SaveChanges()
    {
        var newProduct = new ProductResponse(Product.Id, Name, Description, Price, new Uri(ImagUri));
        await OnlineShopClient.EditProductAsync(newProduct, _cts.Token);
    }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Product = await OnlineShopClient.GetProductAsync(Id, _cts.Token);

        Name = Product.Name;
        Description = Product.Description;
        ImagUri = Product.ImagUri.ToString(); 
        Price = Product.Price;
    }
}
