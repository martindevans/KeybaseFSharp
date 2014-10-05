using System;
using System.Windows;
using Angeronia.Model.Session;
using Angeronia.Model.Settings;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace Angeronia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ISettings _settings;
        public ISettings Settings { get { return _settings; } }

        private readonly ISessionManager _sessionManager;
        public ISessionManager SessionManager { get { return _sessionManager; } }

        public MainWindow(ISettings settings, ISessionManager sessionManager)
        {
            _settings = settings;
            _sessionManager = sessionManager;

            DataContext = this;

            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (_sessionManager.Session == null)
                Login();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);

            if (Settings.ParanoidLocking)
                LogOut();
        }

        private void ToggleLock(object sender, RoutedEventArgs e)
        {
            LogOut();
            Login();
        }

        private async void Login(string message = null, string username = null)
        {
            var credentials = await this.ShowLoginAsync(message ?? "Unlock", "Enter Keybase Credentials", new LoginDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Accented,
                InitialUsername = username ?? _settings.MostRecentlyLoggedInUser,
                UsernameWatermark = "Username",
                PasswordWatermark = "Password",
                AffirmativeButtonText = "Login",
                NegativeButtonText = "Quit",
                AnimateHide = true,
                AnimateShow = true,
                NegativeButtonVisibility = Visibility.Visible,
            });

            if (credentials == null)
                Application.Current.Shutdown();
            else
            {
                var loginProgress = await this.ShowProgressAsync("Logging In", "Validating Login Credentials With Keybase", false);
                var loginResult = await _sessionManager.Login(credentials.Username, credentials.Password);

                await loginProgress.CloseAsync();

                switch (loginResult)
                {
                    case LoginStatus.FailedBadUser:
                        Login("Invalid Username", username = credentials.Username);
                        break;
                    case LoginStatus.FailedBadPassword:
                        Login("Incorrect Password", username = credentials.Username);
                        break;
                    case LoginStatus.InvalidInput:
                        Login("Invalid Credentials", username = credentials.Username);
                        break;
                }
            }
        }

        private void LogOut()
        {
            _sessionManager.LogOut();
        }

        private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsFlyout.IsOpen = true;
        }

        private void ShowPrimaryKeyForUserInTag(object sender, RoutedEventArgs e)
        {
            string user = (string)((Control)sender).Tag;
            var uri = new Uri(new Uri("https://keybase.io"), user + "/key.asc");

            Process.Start(uri.ToString());
        }

        private void NavigateToKeybaseUserInTag(object sender, RoutedEventArgs e)
        {
            string user = (string)((Control)sender).Tag;
            var uri = new Uri(new Uri("https://keybase.io"), user);

            Process.Start(uri.ToString());
        }
    }
}
