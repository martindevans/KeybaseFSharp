using Angeronia.Model.Session;
using Angeronia.Model.Settings;
using Ninject.Modules;

namespace Angeronia.Model
{
    public class ModelModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<ISettings>().To<InMemorySettings>().InSingletonScope().OnActivation(a => a.Load());
            Bind<ISessionManager>().To<InMemorySessionManager>().InSingletonScope().OnActivation(a => a.Load());
        }
    }
}
