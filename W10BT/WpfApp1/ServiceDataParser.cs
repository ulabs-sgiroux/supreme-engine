using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace WpfApp1
{
    public enum ServiceDataTypes
    {
        Flags = 0x01, //   Bluetooth Core Specification:Vol. 3, Part C, section 8.1.3 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.3 and 18.1 (v4.0)Core Specification Supplement, Part A, section 1.3
        //0x02 = Incomplete List of 16-bit Service Class UUIDs   Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.1 and 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        //0x03 = Complete List of 16-bit Service Class UUIDs Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.1 and 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        //0x04 = Incomplete List of 32-bit Service Class UUIDs   Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, section 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        //0x05 = Complete List of 32-bit Service Class UUIDs Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, section 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        //0x06 = Incomplete List of 128-bit Service Class UUIDs  Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.1 and 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        //0x07 = Complete List of 128-bit Service Class UUIDs    Bluetooth Core Specification:Vol. 3, Part C, section 8.1.1 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.1 and 18.2 (v4.0)Core Specification Supplement, Part A, section 1.1
        ShortenedLocalName = 0x08, //    Bluetooth Core Specification:Vol. 3, Part C, section 8.1.2 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.2 and 18.4 (v4.0)Core Specification Supplement, Part A, section 1.2
        CompleteLocalName = 0x09, // Bluetooth Core Specification:Vol. 3, Part C, section 8.1.2 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.2 and 18.4 (v4.0)Core Specification Supplement, Part A, section 1.2
        TxPowerLevel0x0A, //  Bluetooth Core Specification:Vol. 3, Part C, section 8.1.5 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.5 and 18.3 (v4.0)Core Specification Supplement, Part A, section 1.5
        DeviceID = 0x10, //   Device ID Profile v1.3 or later
        //SecurityManagerTKValue = 0x10, //   Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.7 and 18.6 (v4.0)Core Specification Supplement, Part A, section 1.8
        //0x11 = Security Manager Out of Band Flags  Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.6 and 18.7 (v4.0)Core Specification Supplement, Part A, section 1.7
        //0x12 = Slave Connection Interval Range Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.8 and 18.8 (v4.0)Core Specification Supplement, Part A, section 1.9
        //0x14 = List of 16-bit Service Solicitation UUIDs   Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.9 and 18.9 (v4.0)Core Specification Supplement, Part A, section 1.10
        //0x15 = List of 128-bit Service Solicitation UUIDs  Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.9 and 18.9 (v4.0)Core Specification Supplement, Part A, section 1.10
        ServiceData = 0x16, //    Bluetooth Core Specification:Vol. 3, Part C, sections 11.1.10 and 18.10 (v4.0)
        //ServiceData16bitUUID = 0x16, //  Core Specification Supplement, Part A, section 1.11
        Appearance = 0x19, //  Bluetooth Core Specification:Core Specification Supplement, Part A, section 1.12
        AdvertisingInterval = 0x1A, //    Bluetooth Core Specification:Core Specification Supplement, Part A, section 1.15
        LEBluetoothDeviceAddress = 0x1B, // Core Specification Supplement, Part A, section 1.16
        //0x1F = List of 32-bit Service Solicitation UUIDs   Core Specification Supplement, Part A, section 1.10
        ServiceData32bitUUID = 0x20, //  Core Specification Supplement, Part A, section 1.11
        ServiceData128bitUUID = 0x21, // Core Specification Supplement, Part A, section 1.11
        LESecureConnectionsConfirmationValue = 0x22, //    Core Specification Supplement Part A, Section 1.6
        LESecureConnectionsRandomValue = 0x23, //  Core Specification Supplement Part A, Section 1.6
        URI = 0x24, // Bluetooth Core Specification:Core Specification Supplement, Part A, section 1.18
        IndoorPositioning = 0x25, //  Indoor Posiioning Service v1.0 or later
        TransportDiscoveryData = 0x26, //    Transport Discovery Service v1.0 or later
        InformationData3D = 0x3D, //	3D Synchronization Profile, v1.0 or later
        ManufacturerSpecificData = 0xFF, //  Bluetooth Core Specification:Vol. 3, Part C, section 8.1.4 (v2.1 + EDR, 3.0 + HS and 4.0)Vol. 3, Part C, sections 11.1.4 and 18.11 (v4.0)Core Specification Supplement, Part A, section 1.4
    }

    public static class ServiceDataParser
    {
        public static void doit(BluetoothLEAdvertisementDataSection section)
        {
            var interpretedDataType = (ServiceDataTypes)section.DataType;
            switch (interpretedDataType)
            {
                case ServiceDataTypes.Flags:
                    break;
                case ServiceDataTypes.ShortenedLocalName:
                    break;
                case ServiceDataTypes.CompleteLocalName:
                    break;
                case ServiceDataTypes.TxPowerLevel0x0A:
                    break;
                case ServiceDataTypes.DeviceID:
                    break;
                case ServiceDataTypes.ServiceData:
                    break;
                case ServiceDataTypes.Appearance:
                    break;
                case ServiceDataTypes.AdvertisingInterval:
                    break;
                case ServiceDataTypes.LEBluetoothDeviceAddress:
                    break;
                case ServiceDataTypes.ServiceData32bitUUID:
                    break;
                case ServiceDataTypes.ServiceData128bitUUID:
                    break;
                case ServiceDataTypes.LESecureConnectionsConfirmationValue:
                    break;
                case ServiceDataTypes.LESecureConnectionsRandomValue:
                    break;
                case ServiceDataTypes.URI:
                    break;
                case ServiceDataTypes.IndoorPositioning:
                    break;
                case ServiceDataTypes.TransportDiscoveryData:
                    break;
                case ServiceDataTypes.InformationData3D:
                    break;
                case ServiceDataTypes.ManufacturerSpecificData:
                    break;
                default:
                    break;
            }
        }
    }
}
