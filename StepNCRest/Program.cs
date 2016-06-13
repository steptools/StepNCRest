using System;
using Nancy.Hosting.Self;

namespace StepNCRest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a port to run your application. (Press [Enter] for default, Port 8081");
            var port = Console.ReadLine();
            var uri = new Uri("http://127.0.0.1:8081");
            if (port != "")
            {
                uri =
                    new Uri("http://127.0.0.1:" + port);
            }
            HostConfiguration config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = true;
            using (var host = new NancyHost(config,uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
