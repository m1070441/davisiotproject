using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Transport;
using Microsoft.Azure.Devices.Shared;

namespace IoTDevices.Models
{
    public class IoTDeviceProperty
    {
        static string connectionString = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager registryManager= RegistryManager.CreateFromConnectionString(connectionString);
        static DeviceClient Client = null;
        static string DeviceConnectionString= "HostName=NxTIoTTraining.azure-devices.net;DeviceId=davisdt;SharedAccessKey=7rN3Zev33PXuHSBUvpir6e6uRL2oPbtH75Q2MPInzUM=";

        public static async Task UpdateReportedProperties(string deviceName,ReportedProperties properties)
        {
            Client=DeviceClient.CreateFromConnectionString(DeviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            TwinCollection reportedProperties, connectivity;
            reportedProperties= new TwinCollection();
            connectivity = new TwinCollection();
            connectivity["type"] = "cellular";
            reportedProperties["connectivity"] = connectivity;
            reportedProperties["temperature"] = properties.temperature;
            reportedProperties["pressure"] = properties.pressure;
            reportedProperties["drift"] = properties.drift;
            reportedProperties["accuracy"] = properties.accuracy;
            reportedProperties["supplyVoltageLevel"] = properties.supplyVoltageLevel;
            reportedProperties["fullScale"] = properties.fullScale;
            reportedProperties["frequency"] = properties.frequency;
            reportedProperties["resolution"] = properties.resolution;
            reportedProperties["sensorType"] = properties.sensorType;
            reportedProperties["DateTimeLastAppLaunch"] = properties.DateTimeLastAppLaunch;
            await Client.UpdateReportedPropertiesAsync(reportedProperties);
        }

        public static async Task UpdateDesiredProperties(string deviceName)
        {
            Client = DeviceClient.CreateFromConnectionString(DeviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties,telemetryconfig;
            desiredProperties = new TwinCollection();
            telemetryconfig = new TwinCollection();
            telemetryconfig["frequency"] = "5Hz";
            desiredProperties["telemetryconfig"] = telemetryconfig;
            device.Properties.Desired["telemetryConfig"]=telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId, device, device.ETag);
        }

        public static async Task AddTagsAndQuery(string myDeviceId)
        {
            Client = DeviceClient.CreateFromConnectionString(DeviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device = await registryManager.GetTwinAsync(myDeviceId);
            var patch =
        @"{
            tags: {
                location: {
                    region: 'US',
                    plant: 'Redmond43'
                }
            }
        }";
            await registryManager.UpdateTwinAsync(device.DeviceId, patch, device.ETag);
        }

        public static async Task<Twin> GetDevicePropertyAsync(string deviceId)
        {
            var device= await registryManager.GetTwinAsync(deviceId);
            return device;
        }
    }
}
