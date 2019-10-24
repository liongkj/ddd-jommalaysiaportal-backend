
using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Enums;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class OperatingHours : ValueObjectBase
    {

        List<StoreTimes> OperationHours { get; }
        public OperatingHours()
        {
            OperationHours = new List<StoreTimes>();
        }

        public void Add(StoreTimes newOpsHours)
        {
            if (newOpsHours == null) return;

            OperationHours.Add(newOpsHours);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }

    public class StoreTimes
    {
        public DayOfWeekEnum DayOfWeek { get; protected set; }
        public TimeSpan OpenTime { get; protected set; }
        public TimeSpan CloseTime { get; protected set; }

        public StoreTimes(string dayOfWeek, string openTime = "0800", string closeTime = "1700")
        {
            DayOfWeek = DayOfWeekEnum.Parse<DayOfWeekEnum>(dayOfWeek);
            if (DayOfWeek == null)
            {
                throw new ArgumentException("invalid day");
            }

            OpenTime = ConvertTime(openTime);
            CloseTime = ConvertTime(closeTime);
        }

        private TimeSpan ConvertTime(string timeString)
        {
            if (String.IsNullOrWhiteSpace(timeString) && timeString.Length == 4)
            {
                int hour = int.Parse(timeString.Substring(0, 2));
                int minute = int.Parse(timeString.Substring(2, 2));

                var time = new TimeSpan(hour, minute, 0);
                return time;
            }
            throw new InvalidCastException(timeString + " cast failed.");

        }

    }
}