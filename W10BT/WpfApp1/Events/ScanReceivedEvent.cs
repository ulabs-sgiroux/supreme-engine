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
        public List<BluetoothLEManufacturerData> ManufacturerData { get; private set; }
        public List<Guid> ServiceUuids { get; private set; }
        public List<BluetoothLEAdvertisementDataSection> ServiceData { get; private set; }

        public ScanReceivedEventData(BluetoothLEAdvertisementReceivedEventArgs args)
        {
            Address = new BtAddress(args.BluetoothAddress);
            RSSI = args.RawSignalStrengthInDBm;
            ManufacturerData = args.Advertisement.ManufacturerData.ToList();
            ServiceUuids = args.Advertisement.ServiceUuids.ToList();
            ServiceData = args.Advertisement.DataSections.ToList();
        }

        public override string ToString()
        {
            string uuids = string.Join(",", ServiceUuids.ToArray());
            string serviceData = string.Join(",", ServiceData.Select(a => $"[{a.DataType} - {Util.ByteArrayToHexViaLookup32(a.Data.ToArray())}]").ToArray());
            ;
            string manufData = string.Join(",", ManufacturerData.Select(a => $"[{a.CompanyId} - {Util.ByteArrayToHexViaLookup32(a.Data.ToArray())}]").ToArray());
            return $"ScanReceivedEventData: {Address} - [{RSSI}] - {uuids} - {serviceData}";
        }
    }

    public class ScanReceivedEvent : Prism.Events.PubSubEvent<ScanReceivedEventData>
    {
    }
}
