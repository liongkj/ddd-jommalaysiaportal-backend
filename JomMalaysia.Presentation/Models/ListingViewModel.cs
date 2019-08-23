using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Framework.Helper;
using Newtonsoft.Json;

namespace JomMalaysia.Presentation.Models
{
    public class ListingViewModel
    {

        public string MerchantId { get; set; }

        public Status Status { get; set; }


        public ListingType ListingType { get; set; }



        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }


        public CategoryViewModel Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }


    }
}

