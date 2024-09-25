using System.Diagnostics;
using Module02Exercise01.View;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
using System.Net.Http;
using Module02Exercise01.Resources.Styles;

namespace Module02Exercise01
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public static string TestUrl { get; } = "https://www.google.com";
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new WindowsResources());
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());
            }

      MainPage = new AppShell();
            //MainPage = _serviceProvider.GetRequiredService<LoginPage>();
        }

        protected override async void OnStart()
        {
            var current = Connectivity.NetworkAccess;

            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                MainPage = new AppShell();

                Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
                Routing.RegisterRoute(nameof(OfflinePage), typeof(OfflinePage));

                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                MainPage = new AppShell();
                Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
                Routing.RegisterRoute(nameof(OfflinePage), typeof(OfflinePage));

                await Shell.Current.GoToAsync("//OfflinePage");
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("The device is currently on sleep mode.");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("The device has resumed any ongoing activity.");
        }

        private async Task<bool>IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
