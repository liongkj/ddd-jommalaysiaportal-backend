using System.Collections.Generic;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class StoreTimesRequest
    {
        public int Day { get; set; }
        public string CloseTime { get; set; }
        public string OpenTime { get; set; }
    }
}