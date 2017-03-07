using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace WpfApp1.Events
{
    public class ScanReceivedEventData
    {
        public BtAddress Address { get; set; }
        public int RSSI { get; set; }

        public ScanReceivedEventData(BluetoothLEAdvertisementReceivedEventArgs args)
        {
            Address = new BtAddress(args.BluetoothAddress);
            RSSI = args.RawSignalStrengthInDBm;

            foreach (var item in args.Advertisement.DataSections)
            {
                var byteStr = Util.ByteArrayToHexViaLookup32(item.Data.ToArray());
                Debug.WriteLine($"{item.DataType} {byteStr}");
            }
        }
    }

    public class ScanReceivedEvent : Prism.Events.PubSubEvent<ScanReceivedEventData>
    {
    }
}
