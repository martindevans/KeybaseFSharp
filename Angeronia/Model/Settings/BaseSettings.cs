using System.Linq;
using System.Windows;
using MahApps.Metro;
using System.ComponentModel;

namespace Angeronia.Model.Settings
{
    public abstract class BaseSettings
        : ISettings
    {
        private string _accent = "Crimson";
        public string Accent
        {
            get { return _accent; }
            set
            {
                _accent = value;
                RaisePropertyChanged("Accent");
            }
        }

        private string _theme = "BaseDark";
        public string Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                RaisePropertyChanged("Theme");
            }
        }

        private string _mostRecentlyLoggedInUser = null;
        public string MostRecentlyLoggedInUser
        {
            get { return _mostRecentlyLoggedInUser; }
            set
            {
                _mostRecentlyLoggedInUser = SaveMostRecentUser ? value : null;
                RaisePropertyChanged("MostRecentlyLoggedInUser");
            }
        }

        private bool _saveMostRecentuser = true;
        public bool SaveMostRecentUser
        {
            get { return _saveMostRecentuser; }
            set
            {
                _saveMostRecentuser = value;
                if (!value)
                    MostRecentlyLoggedInUser = null;
                RaisePropertyChanged("SaveMostRecentUser");
            }
        }

        bool _paranoidLogout = true;
        public bool ParanoidLogout
        {
            get { return _paranoidLogout; }
            set
            {
                _paranoidLogout = value;
                RaisePropertyChanged("ParanoidLogout");
            }
        }

        bool _paranoidLocking = false;
        public bool ParanoidLocking
        {
            get { return _paranoidLocking; }
            set
            {
                _paranoidLocking = value;
                RaisePropertyChanged("ParanoidLocking");
            }
        }

        bool _paranoidVerification = true;
        public bool ParanoidVerification
        {
            get { return _paranoidVerification; }
            set
            {
                _paranoidVerification = value;
                RaisePropertyChanged("ParanoidVerification");
            }
        }

        protected void ApplyTheme()
        {
            var accent = ThemeManager.Accents.FirstOrDefault(a => a.Name == Accent) ?? ThemeManager.Accents.First(a => a.Name == "Blue");
            ThemeManager.ChangeAppStyle(Application.Current, accent, ThemeManager.GetAppTheme(Theme));
        }

        public virtual void Load()
        {
            ApplyTheme();
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
