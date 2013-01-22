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
            //if (Environment.UserInteractive)
            //{
            //    logger.Info("starting in interactive mode 1");
            //    MainService service = new MainService();
            //    service.Start();
            //}
            //else
            //{
                logger.Info("starting in service mode");
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new MainService() 
                };
                ServiceBase.Run(ServicesToRun);
            //}
        }
    }
}
