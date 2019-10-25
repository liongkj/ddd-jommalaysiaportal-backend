using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetAllListingUseCase : IGetAllListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public GetAllListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public async Task<bool> Handle(GetAllListingRequest message, IOutputPort<GetAllListingResponse> outputPort)
        {
            GetAllListingResponse response;
            try
            {
                response = await _listingRepository.GetAllListings(null).ConfigureAwait(false);
            }

            catch (Exception e)
            {
                response = new GetAllListingResponse(new List<string> { e.ToString() });

            }
            outputPort.Handle(response);

            return response.Success;

        }
    }
}
