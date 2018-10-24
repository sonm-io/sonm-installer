using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbDrivesManager
{
    public class DiskInfo
    {
        public int Index { get; set; }
        public string Model { get; set; }
        public int Partitions { get; set; }
        public Int64 Size { get; set; }
    }
}
