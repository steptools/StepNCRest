using System;
using Nancy.Hosting.Self;

namespace StepNCRest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri =
                new Uri("http://127.0.0.1:8081");

            using (var host = new NancyHost(uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
