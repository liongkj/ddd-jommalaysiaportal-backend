using System.Linq;
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
        private readonly ICategoryRepository _categoryRepository;

        public GetAllListingUseCase(IListingRepository listingRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetAllListingRequest message, IOutputPort<GetAllListingResponse> outputPort)
        {
            GetAllListingResponse response;
            List<ListingViewModel> listingVM;
            try
            {

                var getAllListingResponse = await _listingRepository.GetAllListings(null,false).ConfigureAwait(false);
                listingVM = _mapper.Map<List<ListingViewModel>>(getAllListingResponse.Listings);
                foreach (var l in getAllListingResponse.Listings)
                {
                    var category = await _categoryRepository.FindByNameAsync(l.Category.Category, l.Category.Subcategory);
                    listingVM.Where(x => x.ListingId == l.ListingId).FirstOrDefault().Category = category;
                }

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
