using AvaloniaTester.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using CommunityToolkit.Mvvm.Messaging;
using AvaloniaTester.Messages;

namespace AvaloniaTester.ViewModels.Login
{
    internal partial class LoginViewModel :ViewModelBase
    {
        [ObservableProperty]
        private string _authError = string.Empty;

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private IReadOnlyList<DummyUser> _availableUsers = [];

        [ObservableProperty]
        private DummyUser? _selectedUser = null;

        private readonly ILoginService _loginService;
        private readonly IMessenger _messenger;
        public LoginViewModel(ILoginService loginService, IMessenger messenger)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _ = GetUsers();
        }

        // For design only
        public LoginViewModel() : this(new LoginService(new HttpClient { BaseAddress = new Uri("https://dummyjson.com")}), new WeakReferenceMessenger())
        {
            
        }

        partial void OnSelectedUserChanged(DummyUser? value)
        {
            if(value is null)
            {
                return;
            }

            Username = value.Username;
            Password = value.Password;
        }

        [RelayCommand]        
        protected async Task Login()
        {
            var authResult = await _loginService.Authenticate(Username, Password);
            if(authResult is null)
            {
                AuthError = "Invalid username or password";
                return;
            }

            AuthError = string.Empty;

            _messenger.Send(new LoginSuccessMessage(authResult));
        }

        private async Task GetUsers()
        {
            AvailableUsers = await _loginService.Users();
        }
    }
}
