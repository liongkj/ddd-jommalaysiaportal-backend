using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class CreateListingViewModel
    {
        public string ListingName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ListingType { get; set; }
        public string MerchantId { get; set; }

    }
}
