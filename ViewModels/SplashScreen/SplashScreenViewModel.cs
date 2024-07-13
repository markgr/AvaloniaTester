using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;

namespace AvaloniaTester.ViewModels.SplashScreen
{
    internal partial class SplashScreenViewModel : ViewModelBase
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        public CancellationToken CancellationToken => _cts.Token;

        [ObservableProperty]
        private string _startupMessage = "Starting application...";

        [ObservableProperty]
        private int _progressBarValue = 0;

        [RelayCommand]
        private void CancelCommand()
        {
            StartupMessage = "Cancelling...";
            _cts.Cancel();
        }
    }
}
