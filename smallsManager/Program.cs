using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
                logger.Debug("starting in interactive mode 1");
                MainService service = new MainService();
                service.Start();
                while (true)
                    System.Threading.Thread.Sleep(1000);
            }
            else
            {
                logger.Debug("starting in service mode");
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
