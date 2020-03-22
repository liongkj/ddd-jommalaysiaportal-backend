using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Indexes
{
    public class IndexResponse
    {
         IEnumerable<string> ObjectIDs { get; set; }

          string TaskId { get; set; }
          public bool Success { get; set; }
    }
}