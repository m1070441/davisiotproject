using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoTDevices.Models;

namespace IoTDevices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicePropertiesController : ControllerBase
    {
        [HttpPut]
        [Route("UpdateReportedDeviceProperty")]
        public async Task UpdateReportedDeviceProperty(string deviceName, ReportedProperties properties)
        {
            await IoTDeviceProperty.UpdateReportedProperties(deviceName,properties);
        }

        [HttpPut]
        [Route("UpdateDesiredProperty")]
        public async Task UpdateDesiredProperty(string deviceName)
        {
            await IoTDeviceProperty.UpdateDesiredProperties(deviceName);
        }

        [HttpPut]
        [Route("UpdateTags")]
        public async Task UpdateTags(string deviceName)
        {
            await IoTDeviceProperty.AddTagsAndQuery(deviceName);
        } 
        
        [HttpPost]
        [Route("SendDeviceToCloudMessagesAsync")]
        public async Task SendDeviceToCloudMessagesAsync(string deviceName)
        {
            await IoTDeviceTelemetryMessage.SendDeviceToCloudMessagesAsync(deviceName);
        } 

    }
}