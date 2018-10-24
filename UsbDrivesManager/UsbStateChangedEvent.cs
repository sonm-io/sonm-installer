using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbDrivesManager
{
    public delegate void UsbStateChangedEventHandler(UsbStateChangedEventArgs e);

    public enum StateChange
    {
        Added,
        Removed
    }

    public class UsbStateChangedEventArgs : EventArgs
    {
        public UsbStateChangedEventArgs(int unitMask, StateChange change)
        {
            UnitMask = unitMask;
            Change = change;
        }

        public int UnitMask { get; private set; }

        public StateChange Change { get; private set; }
    }
}
