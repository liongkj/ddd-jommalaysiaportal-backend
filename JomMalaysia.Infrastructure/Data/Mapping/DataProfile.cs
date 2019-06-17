using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;

namespace JomMalaysia.Infrastructure.Data.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            //domain to dto
            CreateMap<Merchant, MerchantDto>()
                .ForMember(md=>md.Id,opt=>opt.MapFrom(m=>m.MerchantId))
                .ForMember(md => md.Contacts, opt => opt.MapFrom(m=> m.Contacts))
                .ForMember(md => md.ListingIds, opt => opt.MapFrom(m => 
                     m.Listings.Select(l=>l.ListingId)));
                
            //.ForMember(m => m.Address, opt => opt.MapFrom(m =>
            //   new AddressDto { Add1 = m.Address.Add1, Add2 = m.Address.Add2, City = m.Address.City, Country = m.Address.Country, PostalCode = m.Address.PostalCode, Region = m.Address.Region }));
            
            
       
            CreateMap<Listing, ListingDto>();
            //dto --> domain

            CreateMap<MerchantDto, Merchant>()
                .ForMember(m => m.MerchantId, opt => opt.MapFrom(md=>md.Id))
                //.ForMember(m => m.Contacts, opt => opt.MapFrom(md => md.Contacts))
                ;
                
               
            CreateMap<ListingDto, Listing>();

        }
    }
}
