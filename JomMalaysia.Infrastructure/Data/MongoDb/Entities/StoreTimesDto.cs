using System;
using System.Collections.Generic;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{

    public class StoreTimesDto
    {
        public int DayOfWeek { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}