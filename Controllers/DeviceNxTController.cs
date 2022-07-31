using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IoTDevices.Models;

namespace IoTDevices.Controllers
{
     [ApiController]
     [Route("[controller]")]
     public class DeviceNxTController : ControllerBase
     {
          [HttpPost]
          [Route("CreateNewDevice")]
          public async Task AddDevice(string deviceId)
          {
               await DeviceNxT.AddDeviceAsync(deviceId);
          }

          [HttpGet]
          [Route("GetDevice")]
          public async Task GetDevice(string deviceId)
          {
               await DeviceNxT.GetDeviceAsync(deviceId);
          }

          [HttpPut]
          [Route("UpdateDevice")]
          public async Task UpdateDevice(string deviceId)
          {
               await DeviceNxT.UpdateDeviceAsync(deviceId);
          }

          [HttpDelete]
          [Route("DeleteDevice")]
          public async Task DeleteDevice(string deviceId)
          {
               await DeviceNxT.RemoveDeviceAsync(deviceId);
          }
     }   
}