using RestSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Angeronia.Model.Session
{
    public class InMemorySession
        : ISession
    {
        public Keybase.User.User User { get; private set; }

        public RestClient Client { get; private set; }

        private ObservableCollection<Tracked> _tracking = new ObservableCollection<Tracked>();
        public ObservableCollection<Tracked> Tracking
        {
            get { return _tracking; }
        }

        public InMemorySession(RestClient session, Keybase.User.User user)
        {
            Client = session;
            User = user;

            Task.Factory.StartNew(GetTrackees);
        }

        private async void GetTrackees()
        {
            var sigs = Keybase.Sig.Sigs(User.Id);
            var tracks = sigs.Sigs.Select(sig => new
            {
                sig,
                json = JObject.Parse(sig.PayloadJson)
            }).Select(a => new
            {
                a.json,
                a.sig,
                track = a.json.SelectToken("body.track")
            }).Where(a => a.track != null).ToArray();

            // ^ this gets *old* track data, need to work out how to get the latest for any particular person

            for (int i = 0; i < 100; i++)
			{
                App.Current.Dispatcher.Invoke(new Action(() => _tracking.Add(new Tracked(i.ToString(), "https://www.gravatar.com/avatar/b20a2b2684e66eceb87a9e57c930649a"))));
                await Task.Delay(200);
			}
        }
    }
}
