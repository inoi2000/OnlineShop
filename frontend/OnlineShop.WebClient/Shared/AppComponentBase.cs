using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineShopHttpApiClient;

namespace OnlineShop.WebClient.Shared
{
    public class AppComponentBase : ComponentBase
    {
        [Inject] protected IOnlineShopClient OnlineShopClient { get; private set; }
        [Inject] protected ILocalStorageService LocalStorage { get; private set; }
        [Inject] protected AppState State { get; private set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (State.IsTokenChecked) return;
            State.IsTokenChecked = true;

            string? token = await LocalStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                OnlineShopClient.SetAuthorizationToken(token);
                State.Account = await OnlineShopClient.GetCurrentUser(_cts.Token);
                State.LoggedIn = true;
            }
            else
            {
                State.LoggedIn = false;
            }
        }
    }
}
