using System;
using System.Collections.Generic;
using System.Linq;

namespace Incapsulation.Failures
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Failure> FailureList { get; set; }
        public override string ToString()
        {
            return $"{Id}, {Name}, {FailureList}";
        }
    }
    public class Failure
    {
        public bool IsFailureSerios => (int)FailureTypeCurrent % 2 == 0;
        public FailureType FailureTypeCurrent { get; set; }
        public DateTime FailureDate { get; set; }
        public override string ToString()
        {
            return $"{FailureDate}, {IsFailureSerios}, {FailureTypeCurrent}";
        }
    }
    public enum FailureType
    {
        UnexpectedShutdown = 0,
        ShortNonRresponding = 1,
        HardwareFailures = 2,
        ConnectionProblems = 3
    }
    public class ReportMaker
    {
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {

            var date = new DateTime(year, month, day);
            var devicesList = new List<Device>();
            foreach (var element in devices)
            {
                var device = new Device()
                {
                    FailureList = new List<Failure>()
                };
                if (element.ContainsKey("DeviceId"))
                {
                    device.Id = int.Parse(element["DeviceId"].ToString());
                }

                if (element.ContainsKey("Name"))
                {
                    device.Name = element["Name"].ToString();
                }
                devicesList.Add(device);
            }

            for (int i = 0; i < deviceId.Length; i++)
            {
                foreach (var device in devicesList)
                {
                    if (device.Id != deviceId[i]) continue;
                    var failure = new Failure()
                    {
                        FailureDate = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
                        FailureTypeCurrent = (FailureType)failureTypes[i]
                    };
                    device.FailureList.Add(failure);
                }
            }
            return FindDevicesFailedBeforeDate(date, devicesList);
        }

        private static List<string> FindDevicesFailedBeforeDate(DateTime obsoleteDateTime, IEnumerable<Device> devices)
        {
            List<string> list = new List<string>();
            foreach (var device in devices)
            {
                foreach (var failure in device.FailureList)
                {
                    if (obsoleteDateTime <= failure.FailureDate || !failure.IsFailureSerios) continue;
                    if (!list.Contains(device.Name)) list.Add(device.Name);
                }
            }
            return list;
        }
    }
}
