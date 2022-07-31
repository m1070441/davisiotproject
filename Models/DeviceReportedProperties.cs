using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTDevices.Models
{
    public class ReportedProperties
    {
        public string temperature { get; set; }
        public string pressure { get; set; }
        public string drift { get; set; }
        public string accuracy { get; set; }
        public string supplyVoltageLevel { get; set; }
        public string fullScale { get; set; }
        public string frequency { get; set; }
        public string resolution { get; set; }
        public string sensorType { get; set; }
        public string DateTimeLastAppLaunch{get; set;}
    }
}