using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;

namespace OnlineShop.WebClient.Pages;

public partial class UserPage
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    public AccountResponse Account { get; set; }

    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            Account = await OnlineShopClient.GetCurrentUser(_cts.Token);
            Snackbar.Add("Успешно", Severity.Success);
            StateHasChanged();
        }
        catch (UnauthorizedAccessException e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }

    }

    public async Task Exit()
    {
        await LocalStorage.RemoveItemAsync("token");
        State.IsTokenChecked = false;
        OnlineShopClient.DeleteAuthorizationToken();
        State.LoggedIn = false;

        NavigationManager.NavigateTo("/authorisation");
    }
}
