using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetAllListingUseCase : IGetAllListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;

        public GetAllListingUseCase(IListingRepository listingRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetAllListingRequest message, IOutputPort<GetAllListingResponse> outputPort)
        {
            GetAllListingResponse response;
            List<ListingViewModel> listingVM;
            try
            {
                var getAllListingResponse = await _listingRepository.GetAllListings(null).ConfigureAwait(false);
                listingVM = _mapper.Map<List<ListingViewModel>>(getAllListingResponse.Listings);
                response = new GetAllListingResponse(listingVM, getAllListingResponse.Success, getAllListingResponse.Message);
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
