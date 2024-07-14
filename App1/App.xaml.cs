using App1.Interfaces;
using App1.Services;
using App1.ViewModels;
using App1.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static IHost HostContainer { get; private set; }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            Frame rootFrame = new Frame();
            RegisterComponents(rootFrame);
            rootFrame.NavigationFailed += RootFrame_NavigationFailed;
            rootFrame.Navigate(typeof(MainPage), args);
            m_window.Content = rootFrame;
            m_window.Activate();
        }

        private void RootFrame_NavigationFailed(object sender, Microsoft.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            throw new Exception($"Error loading page {e.SourcePageType.FullName}");
        }

        private Window m_window;
        private void RegisterComponents(Frame rootFrame)
        {
            var navigationService = new NavigationService(rootFrame);
            navigationService.Configure(nameof(MainPage), typeof(MainPage));
            navigationService.Configure(nameof(ItemDetailsPage), typeof(ItemDetailsPage));
            HostContainer = Host.CreateDefaultBuilder().ConfigureServices(services => {
                services.AddSingleton<INavigationService>(navigationService);
                services.AddSingleton<IDataService, DataService>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<ItemDetailsViewModel>();
            }).Build();
        }
    }
}
