using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace smallsManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Logger logger = LogManager.GetLogger("Program");
            logger.Info("smalls manager is starting up");
            if (Environment.UserInteractive)
            {
                MainService service = new MainService();
                service.Start();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new MainService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
