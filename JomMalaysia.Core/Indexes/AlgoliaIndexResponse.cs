using System;
using System.Collections.Generic;
using Algolia.Search.Models.Common;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Indexes
{
    public class AlgoliaIndexResponse:UseCaseResponseMessage
    {
        BatchIndexingResponse Response { get; }
        
        public IEnumerable<string> Errors { get; }

        public AlgoliaIndexResponse(BatchIndexingResponse response,bool success = false,string message=null):base(success,message)
        {
            Response = response;
        }
        public AlgoliaIndexResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}