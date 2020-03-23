using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Infrastructure.Algolia.Entities;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaDataProfile: Profile
    {
        public AlgoliaDataProfile()
        {
            CreateMap<ListingViewModel, ListingIndexDto>()
                .ForPath(dto => dto.ObjectID, opt => opt.MapFrom(o => o.ListingId))
                .ForPath(dto => dto.Merchant, opt => opt.MapFrom(o => o.Merchant))
                ;

            CreateMap<ListingViewModel.MerchantVM, MerchantIndexDto>()
                ;
        }
    }
}