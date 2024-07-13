using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaTester.ViewModels;
using AvaloniaTester.ViewModels.Login;
using AvaloniaTester.ViewModels.SplashScreen;
using AvaloniaTester.Views;
using AvaloniaTester.Views.Login;
using AvaloniaTester.Views.SplashScreen;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaTester;

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control?>> _locator = new();

    public ViewLocator()
    {
        RegisterViewFactory<LoginSecretViewModel, LoginSecretView>();
        RegisterViewFactory<LoginViewModel, LoginView>();
        RegisterViewFactory<SplashScreenViewModel, SplashScreenView>();
        RegisterViewFactory<ButtonPageViewModel, ButtonPageView>();
        RegisterViewFactory<DataGridPageViewModel, DataGridPageView>();
        RegisterViewFactory<ExpanderPageViewModel, ExpanderPageView>();
        RegisterViewFactory<HomePageViewModel, HomePageView>();
        RegisterViewFactory<ImagePageViewModel, ImagePageView>();
        RegisterViewFactory<MainWindowViewModel, MainWindow>();
        RegisterViewFactory<ShapePageViewModel, ShapePageView>();
        RegisterViewFactory<TextPageViewModel, TextPageView>();        
    }

    public Control? Build(object? data)
    {
        if (data is null)
        {
            return new TextBlock { Text = "No VM provided" };
        }

        _locator.TryGetValue(data.GetType(), out var factory);

        return factory?.Invoke() ?? new TextBlock { Text = $"VM Not Registered: {data.GetType()}" };
    }

    public bool Match(object? data)
    {
        return data is ObservableObject;
    }

    private void RegisterViewFactory<TViewModel, TView>()
        where TViewModel : class
        where TView : Control
        => _locator.Add(
            typeof(TViewModel),
            Design.IsDesignMode
                ? Activator.CreateInstance<TView>
                : Ioc.Default.GetService<TView>);
}