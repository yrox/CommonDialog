using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.Owin.Hosting;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();

            }
        }
    }
}
