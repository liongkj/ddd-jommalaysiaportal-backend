﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
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
                     m.Listings.Select(l => l.ListingId)))
                .ForMember(md => md.CompanyRegistrationNumber, opt => opt.MapFrom(m => m.CompanyRegistrationNumber.ToString()))

                ;


            CreateMap<Listing, ListingDto>()
                .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Category))
                .ForMember(ld => ld.Status, opt => opt.MapFrom(l => l.Status.ToString()))
                .ForMember(ld => ld.ListingType, opt => opt.MapFrom(l => l.ListingType.ToString()))
                .ForMember(ld => ld.Merchant, opt => opt.MapFrom(l => l.Merchant))
                .IncludeAllDerived()
                //.ForMember(ld => ld.Category.Subcategories, opt => opt.MapFrom(l => l.Category.Subcategories))
                ;

            CreateMap<EventListing, ListingDto>()
                ;

            CreateMap<PrivateListing, ListingDto>()
                ;

            CreateMap<Category, CategoryDto>()
                .ForMember(cd => cd.Id, opt => opt.MapFrom(c => c.CategoryId))
                .ForMember(cd => cd.ParentCategory, opt => opt.Ignore())
            //.ForMember(cd=>cd.CategoryPath, opt=>opt.MapFrom(c=>c.CategoryPath.ToString()))
            //.ForMember(cd => cd.Subcategories, opt=> opt.MapFrom(c=>c.Subcategories))
            ;

            CreateMap<Workflow, WorkflowDto>()
                .ForMember(w => w.Id, opt => opt.MapFrom(wd => wd.WorkflowId))
                .ForMember(wd => wd.Status, opt => opt.MapFrom(w => w.Status.ToString()))
                .ForMember(wd => wd.Type, opt => opt.MapFrom(w => w.Type.ToString()))
                ;
            //dto --> domain


            CreateMap<WorkflowDto, Workflow>()
                .ForMember(w => w.WorkflowId, opt => opt.MapFrom(wd => wd.Id))
                .ForMember(w => w.Status, opt => opt.MapFrom(wd => EnumerationBase.Parse<WorkflowStatusEnum>(wd.Status)))
                .ForMember(w => w.Type, opt => opt.MapFrom(wd => EnumerationBase.Parse<WorkflowTypeEnum>(wd.Type)))
                ;

            CreateMap<MerchantDto, Merchant>()
                .ForMember(m => m.MerchantId, opt => opt.MapFrom(md => md.Id))
                .ForMember(m => m.Contacts, opt => opt.MapFrom(md => md.Contacts))
                ;


            CreateMap<ListingDto, Listing>()
                .ForMember(l => l.ListingId, opt => opt.MapFrom(ld => ld.Id))
                .ForMember(l => l.Category, opt => opt.MapFrom(ld => ld.Category))
                .ForMember(l => l.Tags, opt => opt.MapFrom(ld => ld.Tags))
                .ForPath(l => l.Merchant, opt => opt.MapFrom(ld => ld.Merchant))
                .ForMember(l => l.ListingType, opt => opt.MapFrom(ld => ListingTypeEnum.For(ld.ListingType)))
                .ForMember(l => l.Status, opt => opt.MapFrom(ld => ListingStatusEnum.For(ld.Status)))

                .ForMember(l => l.CreatedAt, opt => opt.MapFrom(ld => ld.CreatedAt.ToLocalTime()))
                .ForMember(l => l.ModifiedAt, opt => opt.MapFrom(ld => ld.ModifiedAt.ToLocalTime()))
                .IncludeAllDerived()
                ;

            CreateMap<ListingDto, EventListing>()
                .ForMember(e => e.EventStartDateTime, opt => opt.MapFrom(ld => ld.EventStartDateTime.ToLocalTime()))
                .ForMember(e => e.EventEndDateTime, opt => opt.MapFrom(ld => ld.EventEndDateTime.ToLocalTime()))
                ;

            CreateMap<ListingDto, PrivateListing>()
                ;

            CreateMap<CategoryDto, Category>()
               .ForMember(cd => cd.CategoryId, opt => opt.MapFrom(c => c.Id))

                ;


            //api to domain

        }
    }
}
