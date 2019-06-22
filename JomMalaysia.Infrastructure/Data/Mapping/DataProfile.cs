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
            //.ForMember(cd => cd.Subcategories, opt=> opt.MapFrom(c=>c.Subcategories))
            ;

            CreateMap<Subcategory, SubcategoryDto>()
                    .ForMember(s => s.SubcategoryName, opt => opt.MapFrom(sd => sd.SubcategoryName))
                .ForMember(s => s.SubcategoryNameMs, opt => opt.MapFrom(sd => sd.SubcategoryNameMs))
                .ForMember(s => s.SubcategoryNameZh, opt => opt.MapFrom(sd => sd.SubcategoryNameZh))
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
                .ForMember(c=>c.CategoryId,opt=>opt.MapFrom(cd=>cd.Id))
                .ForMember(c => c.Subcategories, opt => opt.MapFrom(cd => cd.Subcategories))
                ;

            CreateMap<SubcategoryDto, Subcategory>()
                .ForMember(s => s.SubcategoryName, opt => opt.MapFrom(sd => sd.SubcategoryName))
                .ForMember(s => s.SubcategoryNameMs, opt => opt.MapFrom(sd => sd.SubcategoryNameMs))
                .ForMember(s => s.SubcategoryNameZh, opt => opt.MapFrom(sd => sd.SubcategoryNameZh))
            ;



        }
    }
}
