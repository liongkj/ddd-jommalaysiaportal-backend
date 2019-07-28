using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            //domain to dto
            CreateMap<Merchant, MerchantDto>()
                .ForMember(md => md.Id, opt => opt.MapFrom(m => m.MerchantId))
                .ForMember(md => md.Contacts, opt => opt.MapFrom(m => m.Contacts))
                .ForMember(md => md.ListingIds, opt => opt.MapFrom(m =>
                     m.Listings.Select(l => l.ListingId)));


            CreateMap<Listing, ListingDto>()
                .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Category))
                //.ForMember(ld => ld.Category.Subcategories, opt => opt.MapFrom(l => l.Category.Subcategories))
                ;
            
            CreateMap<Category, CategoryDto>()
                .ForMember(cd => cd.Id, opt => opt.MapFrom(c => c.CategoryId))
                .ForMember(cd => cd.ParentCategory, opt => opt.Ignore())
                //.ForMember(cd=>cd.CategoryPath, opt=>opt.MapFrom(c=>c.CategoryPath.ToString()))
            //.ForMember(cd => cd.Subcategories, opt=> opt.MapFrom(c=>c.Subcategories))
            ;

            CreateMap<Workflow, WorkflowDto>()
                .ForMember(wd=>wd.Status,opt=>opt.MapFrom(c=>c.Status.ToString()))
                ;
            //dto --> domain

            CreateMap<MerchantDto, Merchant>()
                .ForMember(m => m.MerchantId, opt => opt.MapFrom(md => md.Id))
                .ForMember(m => m.Contacts, opt => opt.MapFrom(md => md.Contacts))
                ;


            CreateMap<ListingDto, Listing>()
                .ForMember(l => l.Category, opt => opt.MapFrom(ld => ld.Category))
                .ForMember(l => l.Tags, opt => opt.MapFrom(ld => ld.Tags))
                .ForPath(l => l.Merchant.MerchantId, opt => opt.MapFrom(ld => ld.MerchantId))
                ;
            CreateMap<CategoryDto, Category>()
               .ForMember(cd => cd.CategoryId, opt => opt.MapFrom(c => c.Id))

                ;





        }
    }
}
