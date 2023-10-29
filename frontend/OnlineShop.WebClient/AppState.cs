using OnlineShop.HttpModels.Responses;

namespace OnlineShop.WebClient
{
    public class AppState
    {
        public bool IsTokenChecked { get; set; }

        private bool _loggedIn;
        public event Action OnChange;
        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public AccountResponse? Account { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
