﻿using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.Listings.UseCaseResponses
{
    public class CreateListingResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public CreateListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateListingResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}
