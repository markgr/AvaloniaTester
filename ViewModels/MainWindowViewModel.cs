using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace MVVMTest1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
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

        var instance = Activator.CreateInstance(value.ModelType);

        if (instance is null)
            return;

        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "mdi-home"),
        new ListItemTemplate(typeof(ButtonPageViewModel), "mdi-publish"),
        new ListItemTemplate(typeof(ImagePageViewModel), "mdi-image"),
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
