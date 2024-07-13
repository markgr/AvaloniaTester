using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaTester.Services;
using AvaloniaTester.ViewModels;
using AvaloniaTester.ViewModels.Login;
using AvaloniaTester.ViewModels.SplashScreen;
using AvaloniaTester.Views;
using AvaloniaTester.Views.Login;
using AvaloniaTester.Views.SplashScreen;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AvaloniaTester;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // CommunityToolkit will add it's own we need to remove it now
            BindingPlugins.DataValidators.RemoveAt(0);

            // Register all the services 
            var collection = new ServiceCollection();
            ConfigureViewModels(collection);
            ConfigureViews(collection);

            // Add a typed http client factory into the Login service
            collection.AddHttpClient<ILoginService, LoginService>(httpClient => httpClient.BaseAddress = new Uri("https://dummyjson.com"));

            // Inject the IMessage as well
            collection.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            var services = collection.BuildServiceProvider();

            Ioc.Default.ConfigureServices(services);

            //// Create the splash screen
            //var splashScreenVM = new SplashScreenViewModel();
            //var splashScreen = new SplashScreenView
            //{
            //    DataContext = splashScreenVM
            //};

            //// Set as the (temporary) main window.
            //// By default, the application lifetime will shtut down the application when the main
            //// window is closed (unless ShutdownMode is set to something else). Later on we will
            //// swap out MainWindow for the "true" MainWindow before closing the splash screen.
            //// I see that this type of MainWindow swapping is used when switching themes in
            //// Avalonias ControlCatalog source, so presumably it's OK to do this.
            //desktop.MainWindow = splashScreen;

            //splashScreen.Show();

            //try
            //{
            //    // Initialize the device. We can interact with splashScreenVM.CancellationToken
            //    // to determine if the user wants to abort the connection process.
            //    splashScreenVM.StartupMessage = "Searching for devices...";
            //    splashScreenVM.ProgressBarValue = 0;
            //    await Task.Delay(1000, splashScreenVM.CancellationToken);
            //    splashScreenVM.ProgressBarValue = 25;
            //    splashScreenVM.StartupMessage = "Connecting to device #1...";
            //    await Task.Delay(2000, splashScreenVM.CancellationToken);
            //    splashScreenVM.ProgressBarValue = 75;
            //    splashScreenVM.StartupMessage = "Configuring device...";
            //    await Task.Delay(2000, splashScreenVM.CancellationToken);
            //    splashScreenVM.ProgressBarValue = 100;
            //}
            //catch (TaskCanceledException)
            //{
            //    // User cancelled somewhere along the line. We could clean up here if needed.
            //    // Then, close the splash screen and return to shut down the application.
            //    // (If we don't close the splash screen, the app will remain running since
            //    // it is the MainWindow.)
            //    splashScreen.Close();
            //    return;
            //}

            // Create the main window, and swap it in for the real main window
            var vm = Ioc.Default.GetRequiredService<MainWindowViewModel>();

            var mainWin = new MainWindow
            {
                DataContext = vm,
            };

            desktop.MainWindow = mainWin;
            mainWin.Show();

            // Get rid of the splash screen
            //splashScreen.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }

    [Singleton(typeof(MainWindowViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(ButtonPageViewModel))]
    [Transient(typeof(TextPageViewModel))]
    [Transient(typeof(ExpanderPageViewModel))]
    [Transient(typeof(ImagePageViewModel))]
    [Singleton(typeof(DataGridPageViewModel))]
    //[Singleton(typeof(DragAndDropPageViewModel))]
    [Singleton(typeof(SplashScreenViewModel))]
    [Singleton(typeof(LoginSecretViewModel))]
    [Singleton(typeof(LoginViewModel))]
    [Transient(typeof(ShapePageViewModel))]
    internal static partial void ConfigureViewModels(IServiceCollection services);


    [Singleton(typeof(LoginSecretView))]
    [Singleton(typeof(LoginView))]
    [Transient(typeof(ButtonPageView))]
    [Transient(typeof(DataGridPageView))]
    [Transient(typeof(ExpanderPageView))]
    [Transient(typeof(HomePageView))]
    [Transient(typeof(ImagePageView))]
    [Transient(typeof(MainWindow))]
    [Transient(typeof(ShapePageView))]
    [Transient(typeof(TextPageView))]
    [Transient(typeof(SplashScreenView))]
    //[Transient(typeof(ChartsPageView))]
    internal static partial void ConfigureViews(IServiceCollection services);
}