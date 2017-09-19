using Topshelf;
using Topshelf.Nancy;

namespace MessageProcessingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                const string serviceName = "GenericMessageService";
                x.Service<GenericMessageService>(service =>
                {
                    service.ConstructUsing(name => new GenericMessageService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                    service.WithNancyEndpoint(x, nancy =>
                    {
                        nancy.AddHost(port: 80);
                        nancy.OpenFirewallPortsOnInstall(serviceName);
                        nancy.CreateUrlReservationsOnInstall();
                        nancy.DeleteReservationsOnUnInstall();
                    });
                });
                x.RunAsLocalSystem();
                x.SetDescription(serviceName);
                x.SetDisplayName(serviceName);
                x.SetServiceName(serviceName);
                x.RunAsNetworkService();
            });
        }
    }
}
