using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Infrastructure.Algolia.Entities;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaProfile: Profile
    {
        public AlgoliaProfile()
        {
            CreateMap<ListingViewModel, ListingIndexDto>()
                .ForPath(dto => dto.ObjectId, opt => opt.MapFrom(o => o.ListingId))
                .ForPath(dto => dto.Merchant, opt => opt.MapFrom(o => o.Merchant))
                ;

            CreateMap<ListingViewModel.MerchantVM, MerchantIndexDto>()
                ;
        }
    }
}