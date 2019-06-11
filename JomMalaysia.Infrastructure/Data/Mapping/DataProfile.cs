using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;

namespace JomMalaysia.Infrastructure.Data.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Merchant, MerchantDto>().ConstructUsing(u => new MerchantDto { Id = u.MerchantId, FirstName = u.ContactFirstName, LastName = u.ContactLastName });
            CreateMap<MerchantDto, Merchant>().ConstructUsing(au => new Merchant(au.CompanyName, au.CompanyRegistrationNumber,  au.FirstName, au. LastName,  au.Address,  au.Phone,  au.Fax));
        }
    }
}
