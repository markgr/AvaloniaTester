﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTester.ViewModels
{
    internal partial class ButtonPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _isButtonEnabled = true;
    }
}
