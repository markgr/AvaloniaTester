using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTester.ViewModels
{
    internal partial class TextPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isTextEnabled = true;
    }
}
