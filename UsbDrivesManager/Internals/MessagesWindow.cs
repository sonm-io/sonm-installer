using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UsbDrivesManager
{
    internal class MessagesWindow : NativeWindow, IDisposable
    {
        // Logical volume.
        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_VOLUME
        {
            public int dbcv_size;           // size of the struct
            public int dbcv_devicetype;     // DBT_DEVTYP_VOLUME
            public int dbcv_reserved;       // reserved; do not use
            public int dbcv_unitmask;       // Bit 0=A, bit 1=B, and so on (bitmask)
            public short dbcv_flags;        // DBTF_MEDIA=0x01, DBTF_NET=0x02 (bitmask)
        }

        private const int WM_DEVICECHANGE = 0x0219;             // device state change
        private const int DBT_DEVICEARRIVAL = 0x8000;           // detected a new device
        private const int DBT_DEVICEQUERYREMOVE = 0x8001;       // preparing to remove
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;    // removed 
        private const int DBT_DEVTYP_VOLUME = 0x00000002;       // logical volume


        public MessagesWindow()
        {
            base.CreateHandle(new CreateParams());
        }


        public void Dispose()
        {
            base.DestroyHandle();
            GC.SuppressFinalize(this);
        }


        public event UsbStateChangedEventHandler StateChanged;

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if ((message.Msg == WM_DEVICECHANGE) && (message.LParam != IntPtr.Zero))
            {
                DEV_BROADCAST_VOLUME volume = (DEV_BROADCAST_VOLUME) Marshal.PtrToStructure(
                    message.LParam, 
                    typeof(DEV_BROADCAST_VOLUME)
                );

                if (volume.dbcv_devicetype == DBT_DEVTYP_VOLUME)
                {
                    switch (message.WParam.ToInt32())
                    {
                        case DBT_DEVICEARRIVAL:
                            RiseEvent(volume, StateChange.Added);
                            break;

                        case DBT_DEVICEQUERYREMOVE:
                            break;

                        case DBT_DEVICEREMOVECOMPLETE:
                            RiseEvent(volume, StateChange.Removed);
                            break;
                    }
                }
            }
        }


        private void RiseEvent(DEV_BROADCAST_VOLUME volume, StateChange change)
        {
            // char letter = ToDriveLetter(volume.dbcv_unitmask);
            StateChanged?.Invoke(new UsbStateChangedEventArgs(volume.dbcv_unitmask, change));
        }

        /// <summary>
        /// Convert bitmask to drive letter:
        /// 0001 - A
        /// 0010 - B
        /// 0100 - C
        /// and so on. Mask can contain multiple bits on - one bit for each partition on drive.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        //private char ToDriveLetter(int mask)
        //{
        //    const int lettersAmount = 26;
        //    int offset = 0;
        //    while ((offset < lettersAmount) && ((mask & 1) == 0))
        //    {
        //        mask = mask >> 1;
        //        offset++;
        //    }

        //    if (offset < lettersAmount)
        //    {
        //        return Convert.ToChar(Convert.ToInt32('A') + offset);
        //    }

        //    return '?';
        //}
    }
}
