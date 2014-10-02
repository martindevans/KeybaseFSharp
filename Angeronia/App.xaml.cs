using System.Windows;
using Angeronia.Model;
using Angeronia.Model.Session;
using Ninject;

namespace Angeronia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IKernel _container;

        public App()
        {
            _container = new StandardKernel();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container.Load<ModelModule>();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Get<ISessionManager>().LogOut();

            base.OnExit(e);
        }
    }
}
