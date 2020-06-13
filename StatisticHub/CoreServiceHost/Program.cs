using CoreService;
using CoreServiceHost.HeroStatisticService;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace CoreServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HeroStatisticServiceClient heroStatistic = new HeroStatisticServiceClient("BasicHttpBinding_IHeroStatisticService");

            if (heroStatistic.State.Equals(ConnectionState.Open))
            Console.WriteLine(heroStatistic.DbProjectServiceCall());

            List<Type> services = new List<Type>()
            { 
                typeof(HeroStatistic)
                
            };

            ServiceHost host = null;
            List<ServiceHost> hostedServices = new List<ServiceHost>();

            Console.WriteLine("Starting services ..." + Environment.NewLine);

            foreach (Type service in services)
            {
                host = new ServiceHost(service);

                PrintServiceInfo(host);

                hostedServices.Add(host);
            }

            HeroDbService.IHeroDbService dbService = new HeroDbService.HeroDbServiceClient();

            var result = dbService.DbProjectServiceCall();

            Console.WriteLine(result);

            Console.WriteLine("All services were started!");
            Console.WriteLine(Environment.NewLine + "Press any key to close services");
            Console.ReadKey();

            ShutdownServices(hostedServices);            
        }

        private static void PrintServiceInfo(ServiceHost host)
        {
            Console.WriteLine("Service: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 10));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(host.Description.ServiceType);
            Console.ResetColor();

            Console.WriteLine(" launched.");
            Console.WriteLine(Environment.NewLine + "Endpoints:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 10));
            Console.ResetColor();

            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(endpoint.Address);
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void ShutdownServices(List<ServiceHost> services)
        {
            Console.WriteLine(Environment.NewLine + "Closing all services" + Environment.NewLine);

            foreach (ServiceHost service in services)
            {
                Console.Write("Service: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(service.Description.Name);
                Console.ResetColor();
                Console.WriteLine(" is shudowned");

                service.Close();
            }

            Thread.Sleep(3000);
        }
    }
}
