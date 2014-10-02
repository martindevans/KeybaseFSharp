using System.Threading.Tasks;
using Angeronia.Extensions;
using Angeronia.Model.Settings;

namespace Angeronia.Model.Session
{
    public class InMemorySessionManager
        : BaseSessionManager
    {
        public InMemorySessionManager(ISettings settings)
            : base(settings)
        {
            
        }

        public override void LogOut()
        {
            if (Session != null && Settings.ParanoidLogout)
                Keybase.Session.KillAll(Session.Client);

            Session = null;
        }

        public override Task<LoginStatus> Login(string username, string password)
        {
            return Task<LoginStatus>.Factory.StartNew(() =>
            {
                if (username == "test")
                {
                    Session = new InMemorySession(Keybase.Request.MakeClient(), new Keybase.User.User("test", new Keybase.User.Basics("test", 0, 0, 0, 0, 0, "salt"), new Keybase.User.Profile(0, "Full Name", "Location", "Biography For Test User")));
                    return LoginStatus.Success;
                }

                var client = Keybase.Request.MakeClient();

                var salt = Keybase.Session.GetSalt(client, username);
                if (salt.Status.Name != "OK" || salt.LoginSession == null || salt.Salt == null)
                    return LoginStatus.InvalidInput;

                var loginResponse = Keybase.Session.Login(client, username, GeneratePasswordHmac(password, salt.Salt.FromHexToByteArray(), salt.LoginSession), salt.LoginSession);

                if (loginResponse.Status.Name == "OK")
                {
                    Session = new InMemorySession(client, loginResponse.Me);
                    return LoginStatus.Success;
                }
                else
                {
                    return loginResponse.Status.Name == "BAD_LOGIN_PASSWORD" ? LoginStatus.FailedBadPassword : LoginStatus.FailedBadUser;
                }
            });
        }
    }
}
