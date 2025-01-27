﻿using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Entities.Listings.Attractions;
using JomMalaysia.Core.Domain.Entities.Listings.Governments;
using JomMalaysia.Core.Domain.Entities.Listings.Professionals;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;

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
            .ReverseMap()
            ;

            // Domain to dto



            #endregion

            #region listing mapping
            // domain->dto
            CreateMap<Listing, ListingDto>()
                .ForMember(ld => ld.PublishStatus, opt => opt.MapFrom(l => l.PublishStatus))
                .ForMember(ld => ld.Merchant, opt => opt.MapFrom(l => l.Merchant))
                .ForMember(ld => ld.ListingAddress, opt => opt.MapFrom(l => l.Address))
                .IncludeAllDerived()

                ;

            CreateMap<PrivateSector, ListingDto>()
            .IncludeBase<Listing, ListingDto>();

            CreateMap<GovernmentOrg, ListingDto>()
                .IncludeBase<Listing, ListingDto>();

            CreateMap<NonProfitOrg, ListingDto>()
                       .IncludeBase<Listing, ListingDto>();

            CreateMap<Attraction, ListingDto>()
                .IncludeBase<Listing, ListingDto>();

            CreateMap<ProfessionalService, ListingDto>()
                .IncludeBase<Listing, ListingDto>();

            CreateMap<OfficialContact, OfficialContactDto>()
                .ForMember(od => od.OfficeNumber, opt => opt.MapFrom(o => o.OfficeNumber.ToString()))
                .ForMember(od => od.MobileNumber, opt => opt.MapFrom(o => o.MobileNumber.ToString()))
                .ReverseMap();
            //dto->domain
            //mapping parent class
            CreateMap<ListingDto, Listing>()
                        .ForMember(l => l.ListingId, opt => opt.MapFrom(ld => ld.Id))
                        .ForMember(l => l.Tags, opt => opt.MapFrom(ld => ld.Tags))
                        .ForPath(l => l.Merchant, opt => opt.MapFrom(ld => ld.Merchant))
                        .ForMember(l => l.PublishStatus, opt => opt.MapFrom(ld => ld.PublishStatus))
                        .ForMember(l => l.Address, opt => opt.MapFrom(ld => ld.ListingAddress))
                        .ForMember(l => l.OperatingHours, opt => opt.MapFrom(ld => ld.OperatingHours))
                        .ForMember(l => l.CreatedAt, opt => opt.MapFrom(ld => ld.CreatedAt.ToLocalTime()))
                        .ForMember(l => l.ModifiedAt, opt => opt.MapFrom(ld => ld.ModifiedAt.ToLocalTime()))
                        .ForMember(l => l.Category, opt => opt.MapFrom(ld => ld.Category))
                        .IncludeAllDerived()
                           ;
            //map to derived class, need to add new mapping with child properties here

            CreateMap<ListingDto, PrivateSector>()
                .IncludeBase<ListingDto, Listing>();

            CreateMap<ListingDto, GovernmentOrg>()
                .IncludeBase<ListingDto, Listing>();

            CreateMap<ListingDto, NonProfitOrg>()
                .IncludeBase<ListingDto, Listing>();

            CreateMap<ListingDto, Attraction>()
                .IncludeBase<ListingDto, Listing>();

            CreateMap<ListingDto, ProfessionalService>()
                .IncludeBase<ListingDto, Listing>();
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
                                .ForMember(cd => cd.CategoryThumbnail, opt => opt.MapFrom(c => c.CategoryThumbnail))
                                ;
            CreateMap<Image, ImageDto>()
            .ReverseMap();


            CreateMap<CategoryDto, Category>()
                .ForMember(c => c.CategoryId, opt => opt.MapFrom(cd => cd.Id));
            #endregion


            #region workflow mapping

            CreateMap<Workflow, WorkflowDto>()
                    .ForMember(wd => wd.Status, opt => opt.MapFrom(w => w.Status.ToString()))
                    .ForMember(wd => wd.Type, opt => opt.MapFrom(w => w.Type.ToString()))
                    .ForMember(wd => wd.Listing, opt => opt.MapFrom(w => w.Listing))
                    .ReverseMap()
                    ;

            CreateMap<Workflow, WorkflowSummaryDto>()
            .ForMember(wd => wd.Status, opt => opt.MapFrom(w => w.Status.ToString()))

                ;

            CreateMap<WorkflowSummaryDto, Workflow>()
                .ForMember(w => w.Status, opt => opt.MapFrom(wd => WorkflowStatusEnum.For(wd.Status)))
                ;


            //dto --> domain
            CreateMap<WorkflowDto, Workflow>()
                .ForMember(w => w.Status, opt => opt.MapFrom(wd => WorkflowStatusEnum.For(wd.Status)))
                .ForMember(w => w.Type, opt => opt.MapFrom(wd => WorkflowTypeEnum.For(wd.Type)))
                .ForMember(w => w.Listing, opt => opt.Ignore())
                 .ForMember(w => w.Lvl, opt => opt.Ignore())
                 .ForMember(w => w.Requester, opt => opt.MapFrom(wd => wd.Requester))
                 .ForMember(w => w.Responder, opt => opt.MapFrom(wd => wd.Responder))

                ;


            CreateMap<Listing, ListingSummaryDto>()
                           .ForMember(ld => ld.Status, opt => opt.MapFrom(l => l.PublishStatus.Status.ToString()))
                           .ForMember(ld => ld.CategoryType, opt => opt.MapFrom(l => l.CategoryType.ToString()))
                           .ForMember(ld => ld.Merchant, opt => opt.MapFrom(l => l.Merchant))
                           .IncludeAllDerived();


            CreateMap<PrivateSector, ListingSummaryDto>().ReverseMap();
            CreateMap<ProfessionalService, ListingSummaryDto>().ReverseMap();
            CreateMap<GovernmentOrg, ListingSummaryDto>().ReverseMap();
            CreateMap<NonProfitOrg, ListingSummaryDto>().ReverseMap();
            CreateMap<Attraction, ListingSummaryDto>().ReverseMap();

            CreateMap<Merchant, WorkflowMerchantSummaryDto>()
               .ForMember(md => md.SsmId, opt => opt.MapFrom(m => m.CompanyRegistration.SsmId))
               .ForMember(md => md.RegistrationName, opt => opt.MapFrom(m => m.CompanyRegistration.RegistrationName))
               .ReverseMap()
           ;

            CreateMap<ListingSummaryDto, Listing>()
                            .ForMember(l => l.ListingId, opt => opt.MapFrom(ld => ld.ListingId))
                            .ForMember(l => l.PublishStatus, opt => opt.MapFrom(ld => ListingStatusEnum.For(ld.Status)))
                            .ForMember(l => l.Merchant, opt => opt.MapFrom(ld => ld.Merchant))
                            .IncludeAllDerived()

                        ;


            CreateMap<ListingStatusEnum, PublishStatus>()
                    ;


            CreateMap<UserDtoSummary, User>()
            .ForMember(u => u.UserId, opt => opt.MapFrom(ud => ud.UserId))
            .ForMember(u => u.Username, opt => opt.MapFrom(ud => ud.Username))
            .ForMember(u => u.Role, opt => opt.MapFrom(ud => UserRoleEnum.For(ud.Role)))
            .ReverseMap()
            ;


            #endregion

            #region workflowdto to workflowvm
            CreateMap<WorkflowDto, WorkflowViewModel>()
                .ForMember(vm => vm.HistoryData, opt => opt.MapFrom(wd => wd.HistoryData))

            ;
            CreateMap<WorkflowSummaryDto, WorkflowViewModel>()
            ;
            CreateMap<ListingSummaryDto, ListingSummary>()

            ;

            CreateMap<WorkflowSummaryDto, WorkflowSummaryViewModel>();

            CreateMap<WorkflowMerchantSummaryDto, MerchantVM>()
            ;

            CreateMap<UserDtoSummary, UserVM>()
            ;

            #endregion




            //api to domain

        }
    }
}
