using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace WpfApp1
{
    /// <summary>
    /// This type acts as a store for all devices we have seen. It should be updated with the latest information
    /// when available.
    /// 
    /// Currently the [string, string] types will be changed to more appropriate types
    /// </summary>
    public class DeviceDictionary : Dictionary<ulong, BluetoothLEAdvertisementReceivedEventArgs>
    {
    }
}
