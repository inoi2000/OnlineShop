using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MudBlazor;
using OnlineShopHttpApiClient;
using OnlineShopHttpApiClient.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using OnlineShop.HttpModels.Requests;

namespace OnlineShop.WebClient.Pages;

public partial class RegistrationPage
{
    [Inject] private IOnlineShopClient OnlineShopClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private bool _registrationInProgress;

    RegisterRequest model = new();
    bool success;

    private CancellationTokenSource _cts = new CancellationTokenSource();
    private async void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
        await RegisterAccount();
    }

    public async Task RegisterAccount()
    {
        if (success)
        {
            if (_registrationInProgress)
            {
                Snackbar.Add("Пожалуйста подождите!", Severity.Warning);
                return;
            }
            
            try
            {
                _registrationInProgress = true;

                var account = new RegisterRequest(model.Login, model.Email, model.Password, model.ConfirmedPassword);
                await OnlineShopClient.RegisterAccountAsync(account, _cts.Token);

                Snackbar.Add("Регистрация успешно завершена!", Severity.Success);

                NavigationManager.NavigateTo("/catalog");
            }
            catch (OnlineShopApiExeption e)
            {
                Snackbar.Add(e.Details.Title, Severity.Error);
            }
            finally
            {
                _registrationInProgress = false;
                StateHasChanged();
            }
        }
    }
}
