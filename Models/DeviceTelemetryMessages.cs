using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using IoTDevices.Models;
using Microsoft.Azure.Devices.Shared;

namespace IoTDevices.Models
{
    public class IoTDeviceTelemetryMessage
    {
        static string connectionString = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager registryManager= RegistryManager.CreateFromConnectionString(connectionString);
        static DeviceClient Client = null;
        static string deviceConnectionString= "HostName=NxTIoTTraining.azure-devices.net;DeviceId=AS_IoTDevice;SharedAccessKey=lWOyxF3wJ6qz7ng8kllZ84EByjBbCnVD8XpatqIo2s8=";

        public static async Task SendDeviceToCloudMessagesAsync(string deviceName)
        {
            try  
            {  
                //double minTemperature = 20;  
                //double minHumidity = 60;  
                //Random rand = new Random();  
                Client=DeviceClient.CreateFromConnectionString(deviceConnectionString,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                var device= await registryManager.GetTwinAsync(deviceName);
                ReportedProperties properties= new ReportedProperties();
                TwinCollection reportedproperties;
                reportedproperties=device.Properties.Reported;
                while (true)  
                {  
                    //double currentTemperature = minTemperature + rand.NextDouble() * 15;  
                    //double currentHumidity = minHumidity + rand.NextDouble() * 20;    
                    var telemetryDataPoint = new
                    {
                        temperature=reportedproperties["temperature"],
                        pressure=reportedproperties["pressure"],
                        supplyVoltageLevel=reportedproperties["supplyVoltageLevel"],
                        fullScale=reportedproperties["fullScale"],
                        frequency=reportedproperties["frequency"],
                        accuracy=reportedproperties["accuracy"],
                        resolution=reportedproperties["resolution"],
                        drift=reportedproperties["drift"],
                        sensorType=reportedproperties["sensorType"],
                    };
                    string messageString = "";   
                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);   
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));  
                    await Client.SendEventAsync(message);  
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);  
                    await Task.Delay(1000 * 10);                    
                }  
            }  
            catch (Exception ex)  
            {   
                throw ex;  
            } 
        }
    }
}