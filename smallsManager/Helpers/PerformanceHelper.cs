using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace smallsManager
{
    public static class PerformanceHelper
    {
        

        public static float GetTotalCpuUsage()
        {
            using (PerformanceCounter cpuCounter = new PerformanceCounter()
                    {
                        CategoryName = "Processor",
                        CounterName = "% Processor Time",
                        InstanceName = "_Total"
                    }
            )
            {
                cpuCounter.NextValue();
                System.Threading.Thread.Sleep(500);
                return cpuCounter.NextValue();
            }
        }
    }
}
