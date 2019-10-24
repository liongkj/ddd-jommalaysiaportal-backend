using System;
using System.Collections.Generic;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class OperatingHoursDto
    {
        List<StoreTimesDto> OperationHours { get; set; }
    }

    public class StoreTimesDto
    {
        public int Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}