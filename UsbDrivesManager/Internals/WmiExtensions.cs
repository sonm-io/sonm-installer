using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace UsbDrivesManager
{
    internal static class WmiExtensions
    {
        public static ManagementObject FirstOrDefault(this ManagementObjectSearcher searcher)
        {
            return searcher.Get().OfType<ManagementObject>().FirstOrDefault();
        }
    }
}
