using System;
using System.Collections.Generic;
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
                .ForPath(m => m.FirstName, opt => opt.MapFrom(md => md.ContactName.FirstName))
                .ForPath(m => m.LastName, opt => opt.MapFrom(md => md.ContactName.LastName))
                .ForMember(m=>m.ContactEmail,opt=>opt.MapFrom(md=>md.ContactEmail.ToString()))
            .ForMember(m => m.Address, opt => opt.MapFrom(m =>
               new AddressDto { Add1 = m.Address.Add1, Add2 = m.Address.Add2, City = m.Address.City, Country = m.Address.Country, PostalCode = m.Address.PostalCode, Region = m.Address.Region }));
            
            CreateMap<Address, AddressDto>();
       
            CreateMap<Listing, ListingDto>();
            //dto --> domain
            CreateMap<AddressDto, Address>();
            CreateMap<MerchantDto, Merchant>()
                .ForMember(md=> md.Address,opt => opt.MapFrom(m=>m.Address))
                .ForMember(md=>md.ContactEmail,opt=>opt.MapFrom(m=>m.ContactEmail));
            CreateMap<ListingDto, Listing>();

        }
    }
}
