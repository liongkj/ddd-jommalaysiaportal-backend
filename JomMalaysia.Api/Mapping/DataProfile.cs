using AutoMapper;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Api.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<BaseListingHolder, Listing>()
                .ConstructUsing(m => ListingFactory.CreateListing(ListingTypeEnum.For(m.ListingType)))
                .ForMember(m=>m.ListingType,opt=>opt.MapFrom(l=>ListingTypeEnum.For(l.ListingType)))
                .ForMember(m=>m.Category,opt=>opt.MapFrom(l=>new CategoryPath(l.Category,l.Subcategory)))
                ;

            CreateMap<EventListingHolder, EventListing>()
               .IncludeBase<BaseListingHolder,Listing>()
               ;

            CreateMap<ListingHolder, PrivateListing>();

        }
    }
}
