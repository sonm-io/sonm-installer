using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace UsbDrivesManager
{
    public class UsbManager : IDisposable
    {
        private delegate void GetDiskInfoDelegate(DiskInfo disk);

        private MessagesWindow window;
        private UsbStateChangedEventHandler handler;

        public event UsbStateChangedEventHandler StateChanged
        {
            add
            {
                if (window == null)
                {
                    window = new MessagesWindow();
                    window.StateChanged += new UsbStateChangedEventHandler(InvokeHandler);
                }

                handler = (UsbStateChangedEventHandler)Delegate.Combine(handler, value);
            }

            remove
            {
                handler = (UsbStateChangedEventHandler)Delegate.Remove(handler, value);

                if (handler == null)
                {
                    window.StateChanged -= new UsbStateChangedEventHandler(InvokeHandler);
                    window.Dispose();
                    window = null;
                }
            }
        }

        public void Dispose()
        {
            if (window != null)
            {
                window.StateChanged -= new UsbStateChangedEventHandler(InvokeHandler);
                window.Dispose();
                window = null;
            }
        }

        public IEnumerable<DiskInfo> GetUsbDrives ()
        {
            var query = new WqlObjectQuery("select DeviceID, Caption, Description, Index, MediaType, Model, Partitions, Size from Win32_DiskDrive where InterfaceType='USB'");
            using (var searcher = new ManagementObjectSearcher(query))
            {
                return searcher.Get().OfType<ManagementObject>().Select(MapToDiskInfo);
            }
        }

        private DiskInfo MapToDiskInfo (ManagementObject mo)
        {
            return new DiskInfo()
            {
                Index = int.Parse(GetStr(mo, "Index")),
                Model = GetStr(mo, "Model"),
                Partitions = int.Parse(GetStr(mo, "Partitions")),
                Size = long.Parse(GetStr(mo, "Size"))
            };
        }

        private string GetStr(ManagementObject mo, string propName)
        {
            return mo.Properties[propName].Value.ToString();
        }

        private void InvokeHandler(UsbStateChangedEventArgs e)
        {
            handler?.Invoke(e);
        }

        ~UsbManager()
        {
            Dispose();
        }
    }
}
