using LiveScoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(LiveScoreService);
            Uri http = new Uri("http://localhost:8000/LiveScoreService");
            ServiceHost host = new ServiceHost(t, http);
            host.Open();
            Console.WriteLine("Live Cricket System");
            Console.WriteLine("Service is live.....");
            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
            host.Close();
        }
    }
}
