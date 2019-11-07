using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows;
using JomMalaysia.Core.Domain.Entities.Listings;
using System.Collections.Generic;
using JomMalaysia.Infrastructure.Auth0.Entities;

namespace JomMalaysia.Infrastructure.Data.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            //domain to dto
            #region merchant mapping
            CreateMap<Merchant, MerchantDto>()
                .ForMember(md => md.Id, opt => opt.MapFrom(m => m.MerchantId))
                .ForMember(md => md.Contacts, opt => opt.MapFrom(m => m.Contacts))
                .ForMember(md => md.ListingIds, opt => opt.MapFrom(m => m.Listings))
                .ForMember(md => md.CompanyRegistration, opt => opt.MapFrom(m => m.CompanyRegistration))
                ;

            //dto to domain 
            CreateMap<MerchantDto, Merchant>()
                .ForMember(m => m.MerchantId, opt => opt.MapFrom(md => md.Id))
                //.ForMember(m => m.Contacts, opt => opt.MapFrom(md => md.Contacts))
                .ForMember(m => m.Listings, opt => opt.MapFrom(m => m.ListingIds))
                .ForMember(m => m.CompanyRegistration, opt => opt.MapFrom(md => CompanyRegistration.For(md.CompanyRegistration.SsmId, md.CompanyRegistration.RegistrationName, md.CompanyRegistration.OldSsmId)))

                 ;

            CreateMap<Merchant, MerchantSummaryDto>()
            .ForMember(msd => msd.Id, opt => opt.MapFrom(m => m.MerchantId))
            .ForMember(msd => msd.CompanyName, opt => opt.MapFrom(m => m.CompanyRegistration.RegistrationName))
            .ForMember(msd => msd.SsmId, opt => opt.MapFrom(m => m.CompanyRegistration.SsmId))
            .ReverseMap()
            ;


            #endregion

            #region contacts mapping

            //domain to dto
            CreateMap<Contact, ContactsDto>()
            .ForMember(cd => cd.Name, opt => opt.MapFrom(c => c.Name.ToString()))
            .ForMember(cd => cd.Email, opt => opt.MapFrom(c => c.Email.ToString()))
            .ForMember(cd => cd.Phone, opt => opt.MapFrom(c => c.Phone.ToString()))
            ;

            CreateMap<ContactsDto, Contact>()
            .ForMember(c => c.Phone, opt => opt.MapFrom(cd => (Phone)cd.Phone))
            .ForMember(c => c.Email, opt => opt.MapFrom(cd => cd.Email))
            .ForMember(c => c.Name, opt => opt.MapFrom(cd => (Name)cd.Name))
            ;

            // Domain to dto



            #endregion

            #region listing mapping
            // domain->dto
            CreateMap<Listing, ListingDto>()
                .ForMember(ld => ld.PublishStatus, opt => opt.MapFrom(l => l.PublishStatus))
                .ForMember(ld => ld.ListingType, opt => opt.MapFrom(l => l.ListingType.ToString()))
                .ForMember(ld => ld.Merchant, opt => opt.MapFrom(l => l.Merchant))
                .ForMember(ld => ld.ListingAddress, opt => opt.MapFrom(l => l.Address))
                .IncludeAllDerived()

                ;



            CreateMap<EventListing, ListingDto>()
            .IncludeBase<Listing, ListingDto>()
            .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Category))
                    ;

            CreateMap<LocalListing, ListingDto>()
            .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Category))
            .IncludeBase<Listing, ListingDto>()
                ;

            CreateMap<AdministrativeListing, ListingDto>()
                       .IncludeBase<Listing, ListingDto>()

                               ;

            //dto->domain
            //mapping parent class
            CreateMap<ListingDto, Listing>()
                        .ForMember(l => l.ListingId, opt => opt.MapFrom(ld => ld.Id))
                        .ForMember(l => l.Tags, opt => opt.MapFrom(ld => ld.Tags))
                        .ForPath(l => l.Merchant, opt => opt.MapFrom(ld => ld.Merchant))
                        .ForMember(l => l.ListingType, opt => opt.MapFrom(ld => ListingTypeEnum.For(ld.ListingType)))
                        .ForMember(l => l.PublishStatus, opt => opt.MapFrom(ld => ld.PublishStatus))
                        .ForMember(l => l.Address, opt => opt.MapFrom(ld => ld.ListingAddress))
                        .ForMember(l => l.OperatingHours, opt => opt.MapFrom(ld => ld.OperatingHours))
                        .ForMember(l => l.CreatedAt, opt => opt.MapFrom(ld => ld.CreatedAt.ToLocalTime()))
                        .ForMember(l => l.ModifiedAt, opt => opt.MapFrom(ld => ld.ModifiedAt.ToLocalTime()))
                        .IncludeAllDerived()
                           ;
            //map to derived class, need to add new mapping with child properties here
            CreateMap<ListingDto, EventListing>()
                    .ForMember(e => e.EventStartDateTime, opt => opt.MapFrom(ld => ld.EventStartDateTime.ToLocalTime()))
                    .ForMember(e => e.EventEndDateTime, opt => opt.MapFrom(ld => ld.EventEndDateTime.ToLocalTime()))
                    .ForMember(l => l.Category, opt => opt.MapFrom(ld => ld.Category))
                    .IncludeBase<ListingDto, Listing>()

                    ;

            CreateMap<ListingDto, LocalListing>()
                .IncludeBase<ListingDto, Listing>()
                .ForMember(l => l.Category, opt => opt.MapFrom(ld => ld.Category))

                ;

            CreateMap<ListingDto, AdministrativeListing>()

                .IncludeBase<ListingDto, Listing>()
            ;

            #endregion

            #region map publish status
            CreateMap<PublishStatus, PublishStatusDto>()
                .ForMember(pd => pd.Status, opt => opt.MapFrom(p => p.Status.ToString()))
                .ForMember(pd => pd.ValidityStart, opt => opt.MapFrom(p => p.ValidityStart))
                .ForMember(pd => pd.ValidityEnd, opt => opt.MapFrom(p => p.ValidityEnd))

            ;

            CreateMap<PublishStatusDto, PublishStatus>()
           .ForMember(p => p.Status, opt => opt.MapFrom(pd => ListingStatusEnum.For(pd.Status)))
           .ForMember(p => p.ValidityStart, opt => opt.MapFrom(pd => pd.ValidityStart))
           .ForMember(p => p.ValidityEnd, opt => opt.MapFrom(pd => pd.ValidityEnd))

       ;
            #endregion

            #region map operatinghours


            CreateMap<StoreTimes, StoreTimesDto>()
                .ForMember(sd => sd.DayOfWeek, opt => opt.MapFrom(s => s.DayOfWeek.ToInt()))
                .ForMember(sd => sd.CloseTime, opt => opt.MapFrom(s => s.CloseTime.ToString()))
                .ForMember(sd => sd.CloseTime, opt => opt.MapFrom(s => s.CloseTime.ToString()))
                .ReverseMap()
            ;

            #endregion

            #region map address

            CreateMap<Address, AddressDto>()
            .ForMember(ad => ad.Location, opt => opt.MapFrom(a => a.Location.ToGeoJsonPoint()))
            .ReverseMap()
            ;

            CreateMap<AddressDto, Address>()
                .ForMember(a => a.Location, opt => opt.MapFrom(ad => new Location(
                    new Coordinates(ad.Location.Coordinates.Longitude, ad.Location.Coordinates.Latitude))))
                ;

            // CreateMap<Address, AddressDto>()
            //     .ForMember(a => a.Location, opt => opt.MapFrom(ad => ad.Location))
            //     ;
            #endregion

            #region category
            CreateMap<Category, CategoryDto>()
                                .ForMember(cd => cd.Id, opt => opt.MapFrom(c => c.CategoryId))
                                .ForMember(cd => cd.ParentCategory, opt => opt.Ignore())
                                ;



            CreateMap<CategoryDto, Category>()
               .ForMember(c => c.CategoryId, opt => opt.MapFrom(cd => cd.Id))
                ;
            //.ForMember(cd=>cd.CategoryPath, opt=>opt.MapFrom(c=>c.Cate
            //.ForMember(cd=>cd.CategoryPath, opt=>opt.MapFrom(c=>c.CategoryPath.ToString()))
            //.ForMember(cd => cd.Subcategories, opt=> opt.MapFrom(c=>c.Subcategories))
            ;
            #endregion


            #region workflow mapping

            CreateMap<Workflow, WorkflowDto>()
                    .ForMember(wd => wd.Id, opt => opt.MapFrom(w => w.WorkflowId))
                    .ForMember(wd => wd.Status, opt => opt.MapFrom(w => w.Status.ToString()))
                    .ForMember(wd => wd.Type, opt => opt.MapFrom(w => w.Type.ToString()))
                    .ForMember(wd => wd.Listing, opt => opt.MapFrom(w => w.Listing))
                    .ForMember(wd => wd.Merchant, opt => opt.MapFrom(w => w.Listing.Merchant.CompanyRegistration.RegistrationName))
                    ;

            CreateMap<Workflow, WorkflowSummaryDto>()
            .ReverseMap()
                ;



            //dto --> domain
            CreateMap<WorkflowDto, Workflow>()
                .ForMember(w => w.WorkflowId, opt => opt.MapFrom(wd => wd.Id))
                .ForMember(w => w.Status, opt => opt.MapFrom(wd => EnumerationBase.Parse<WorkflowStatusEnum>(wd.Status)))
                .ForMember(w => w.Type, opt => opt.MapFrom(wd => EnumerationBase.Parse<WorkflowTypeEnum>(wd.Type)))
                .ForMember(w => w.Listing, opt => opt.Ignore())
                 .ForMember(w => w.Lvl, opt => opt.Ignore())
                 .ForMember(w => w.Requester, opt => opt.MapFrom(wd => wd.Requester))
                 .ForMember(w => w.Responder, opt => opt.MapFrom(wd => wd.Responder))

                ;


            CreateMap<Listing, ListingSummaryDto>()
                           .ForMember(ld => ld.Status, opt => opt.MapFrom(l => l.PublishStatus.Status.ToString()))
                           .ForMember(ld => ld.ListingType, opt => opt.MapFrom(l => l.ListingType.ToString()))
                           .IncludeAllDerived()
                       ;

            CreateMap<EventListing, ListingSummaryDto>();
            CreateMap<LocalListing, ListingSummaryDto>();
            CreateMap<ListingSummaryDto, Listing>()
                            .ForMember(l => l.ListingId, opt => opt.MapFrom(ld => ld.Id))
                            .ForMember(l => l.ListingType, opt => opt.MapFrom(ld => ListingTypeEnum.For(ld.ListingType)))
                            .ForMember(l => l.PublishStatus, opt => opt.MapFrom(ld => ListingStatusEnum.For(ld.Status)))

                            .IncludeAllDerived()

                        ;

            CreateMap<ListingSummaryDto, EventListing>()
                        ;

            CreateMap<ListingSummaryDto, LocalListing>();

            CreateMap<UserDtoSummary, User>()
            .ForMember(u => u.UserId, opt => opt.MapFrom(ud => ud.UserId))
            .ForMember(u => u.Username, opt => opt.MapFrom(ud => ud.Username))
            .ReverseMap()
            ;


            #endregion






            //api to domain

        }
    }
}
