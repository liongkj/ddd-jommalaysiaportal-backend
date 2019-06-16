﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class GetAllMerchantRequest:IUseCaseRequest<GetAllMerchantResponse>
    {
       public ICollection<Merchant> Merchants { get; set; }

        public GetAllMerchantRequest()
        {
            Merchants = new Collection<Merchant>();
        }
    }
}
