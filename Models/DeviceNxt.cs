using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace IoTDevices.Models
{
    public class DeviceNxT
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static Device device;

        public static async Task<Device> AddDeviceAsync(string deviceId)
        {
            registryManager= RegistryManager.CreateFromConnectionString(connectionString);
            device= await registryManager.AddDeviceAsync(new Device(deviceId));
            return device;
        }
        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            registryManager= RegistryManager.CreateFromConnectionString(connectionString);
            device= await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
        public static async Task UpdateDeviceAsync(string deviceId)
        {
            Device device1;
            registryManager= RegistryManager.CreateFromConnectionString(connectionString);
            device1= await registryManager.GetDeviceAsync(deviceId);           
            device1.Status=Microsoft.Azure.Devices.DeviceStatus.Enabled;        
            await registryManager.UpdateDeviceAsync(device1);
            
        }
        public static async Task RemoveDeviceAsync(string deviceId)
        {
            registryManager= RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);           
        }
    }
}