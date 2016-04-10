using System.ServiceProcess;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ProxyService
{
    static class Program
    {
        static void Main()
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                container.Resolve<ProxyService>()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
