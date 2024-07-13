using AvaloniaTester.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTester.ViewModels.Login
{
    internal partial class LoginSecretViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _token = string.Empty;

        public LoginSecretViewModel(AuthenticationResult authResult)
        {
            Token = authResult.Token;
        }
    }
}
