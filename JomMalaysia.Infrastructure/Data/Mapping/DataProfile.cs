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
                //.ForPath(m => m.FirstName, opt => opt.MapFrom(md => md.ContactName.FirstName))
                //.ForPath(m => m.LastName, opt => opt.MapFrom(md => md.ContactName.LastName))
                .ForMember(md => md.Contacts, opt => opt.MapFrom(m=> m.Contacts))
                .ForMember(md => md.Listings, opt => opt.MapFrom(m => m.Listings ) ) ;
                
            //.ForMember(m => m.Address, opt => opt.MapFrom(m =>
            //   new AddressDto { Add1 = m.Address.Add1, Add2 = m.Address.Add2, City = m.Address.City, Country = m.Address.Country, PostalCode = m.Address.PostalCode, Region = m.Address.Region }));
            
            
       
            CreateMap<Listing, ListingDto>();
            //dto --> domain

            CreateMap<MerchantDto, Merchant>()
                .ForMember(m => m.Contacts, opt => opt.MapFrom(md => md.Contacts))
                .ForMember(m => m.Listings, opt => opt.MapFrom(md => md.Listings));
               
            CreateMap<ListingDto, Listing>();

        }
    }
}
