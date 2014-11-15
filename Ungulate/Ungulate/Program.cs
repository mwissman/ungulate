using System;
using Microsoft.Owin.Hosting;

namespace Ungulate
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9080/";

            
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Ungulate is running");
                Console.WriteLine("Press any key to close...");
                Console.ReadLine();
            }

        }
    }
}
