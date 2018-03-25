using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WatcherService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string[] args = System.Environment.GetCommandLineArgs();
#if CONSOLE
            Console.WriteLine("Press any key to quit");

            Service svc = new Service();
            svc.Start(args);
            Console.ReadKey();
            svc.Stop();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }

    }
}
