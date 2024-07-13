using Avalonia.Controls;
using AvaloniaTester.Messages;
using AvaloniaTester.ViewModels.Login;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaTester.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(IMessenger messenger)
    {
        messenger.Register<MainWindowViewModel, LoginSuccessMessage>(this, (_, message) =>
        {
            CurrentPage = new LoginSecretViewModel(message.Value);
        });
    }

    public MainWindowViewModel() : this(new WeakReferenceMessenger())
    {
        
    }

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null)
            return;

        var instance = Design.IsDesignMode ? Activator.CreateInstance(value.ModelType) : Ioc.Default.GetService(value.ModelType);

        if (instance is null)
            return;

        CurrentPage = (ViewModelBase)instance;
    }    

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "mdi-home"),
        new ListItemTemplate(typeof(ButtonPageViewModel), "mdi-publish"),
        new ListItemTemplate(typeof(ImagePageViewModel), "mdi-image"),
        new ListItemTemplate(typeof(ShapePageViewModel), "mdi-square"),
        new ListItemTemplate(typeof(TextPageViewModel), "mdi-format-text"),
        new ListItemTemplate(typeof(DataGridPageViewModel), "mdi-grid-large"),
        new ListItemTemplate(typeof(ExpanderPageViewModel), "mdi-arrow-expand-down"),
        new ListItemTemplate(typeof(LoginViewModel), "mdi-login"),
    };

    [RelayCommand]
    private void PaneButton()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string icon)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
        IconName = icon;
    }

    public string Label { get; set; }
    public Type ModelType { get; set; }
    public string IconName { get; set; }
}
