
using RestSharp;
using System.Collections.ObjectModel;
namespace Angeronia.Model.Session
{
    public interface ISession
    {
        RestClient Client { get; }

        Keybase.User.User User { get; }

        ObservableCollection<Tracked> Tracking { get; }
    }
}
