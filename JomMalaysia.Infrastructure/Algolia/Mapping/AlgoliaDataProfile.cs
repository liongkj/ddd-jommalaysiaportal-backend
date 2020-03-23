using AutoMapper;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.SharedRequest;
using JomMalaysia.Infrastructure.Algolia.Entities;

namespace JomMalaysia.Infrastructure.Algolia.Mapping
{
    public class AlgoliaDataProfile: Profile
    {
        public AlgoliaDataProfile()
        {
            CreateMap<ListingViewModel, ListingIndexDto>()
                .ForPath(dto => dto.ObjectID, opt => opt.MapFrom(o => o.ListingId))
                .ForPath(dto => dto.Merchant, opt => opt.MapFrom(o => o.Merchant))
                .ForPath(dto=>dto.Photo,opt
                    =>opt.MapFrom(o=>o.ListingImages.ListingLogo.Url))
                .ForPath(dto=>dto._geoloc,opt=>opt.MapFrom(o=>o.Address.Coordinates))
                .ForPath(dto=>dto.Category,
                    opt=>opt.MapFrom(o=>o.Category))
                ;

            CreateMap<ListingViewModel.CategoryPathViewModel,CategoryIndexDto>()
                .ForPath(dto=>dto.En,
                    opt=>opt.MapFrom(o=>o.getCategoryFor("en")))
                
                .ForPath(dto=>dto.Zh,
                    opt=>opt.MapFrom(o=>o.getCategoryFor("zh")))
                
                .ForPath(dto=>dto.Ms,
                    opt=>opt.MapFrom(o=>o.getCategoryFor("ms")))
                ;
            
            CreateMap<Coordinates, GeoIndexDto>()
                .ForMember(dto => dto.lat, opt 
                    => opt.MapFrom(o => o.Latitude))
                .ForMember(dto => dto.lng, opt => opt.MapFrom(o => o.Longitude));

            CreateMap<ListingViewModel.MerchantVM, MerchantIndexDto>()
                ;
            
            CreateMap<AddressViewModel, AddressIndexDto>();
            
           
        }
    }
}