using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using NLog;

namespace smallsManager
{
    public static class WmiHelper
    {        
        public static bool IsPowerOnline()
        {
            try
            {
                ManagementClass mc = new ManagementClass("/ROOT/wmi:BatteryStatus");
                ManagementObjectCollection moc = mc.GetInstances();
                if (moc.Count != 0)
                {
                    foreach (var mo in moc)
                    {
                        if (mo["PowerOnLine"] != null && (bool)mo["PowerOnline"] == true)
                            return true;
                        else if (mo["PowerOnLine"] != null && (bool)mo["PowerOnline"] == false)
                            return false;
                    }
                    return false;
                }
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
