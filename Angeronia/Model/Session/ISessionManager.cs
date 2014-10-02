using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Angeronia.Model.Session
{
    public interface ISessionManager : INotifyPropertyChanged
    {
        ISession Session { get; }

        bool IsLoggedIn { get; }

        string CurrentlyLoggedInUsername { get; }

        void Load();

        void LogOut();

        Task<LoginStatus> Login(string username, string password);
    }

    public enum LoginStatus
    {
        Success,
        InvalidInput,
        FailedBadUser,
        FailedBadPassword
    }
}
