using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class ListingWorkflowRequest : IUseCaseRequest<ListingWorkflowResponse>
    {
        public string ListingId { get; set; }
        public int Months { get; set; } = 12;

    }
}
