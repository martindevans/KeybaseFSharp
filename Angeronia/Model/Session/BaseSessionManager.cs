using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angeronia.Extensions;
using Angeronia.Model.Settings;
using CryptSharp;
using CryptSharp.Utility;

namespace Angeronia.Model.Session
{
    public abstract class BaseSessionManager
        : ISessionManager
    {
        protected ISettings Settings { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Load()
        {
        }

        private ISession _session;
        public ISession Session
        {
            get { return _session; }
            protected set
            {
                _session = value;

                if (_session != null)
                    Settings.MostRecentlyLoggedInUser = _session.User.Basics.Username;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Session"));
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoggedIn"));
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentlyLoggedInUsername"));
                }
            }
        }

        public bool IsLoggedIn
        {
            get { return _session != null; }
        }

        public string CurrentlyLoggedInUsername
        {
            get { return _session == null ? "" : _session.User.Basics.Username; }
        }

        protected BaseSessionManager(ISettings settings)
        {
            Settings = settings;
        }

        public abstract void LogOut();

        public abstract Task<LoginStatus> Login(string username, string password);

        protected static string GeneratePasswordHmac(string password, byte[] salt, string loginSession)
        {
            var pwh = SCrypt.ComputeDerivedKey(Encoding.UTF8.GetBytes(password), salt, 32768, 8, 1, 1, 224).Skip(192).Take(224 - 192).ToArray();
            var hmac = new System.Security.Cryptography.HMACSHA512(pwh);
            return hmac.ComputeHash(Convert.FromBase64String(loginSession)).ToHexString();
        }
    }
}
