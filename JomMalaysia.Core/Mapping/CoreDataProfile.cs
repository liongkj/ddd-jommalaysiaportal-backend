using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.UserUseCase.Get;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Framework.Helper;
using static JomMalaysia.Core.UseCases.ListingUseCase.Get.ListingViewModel;

namespace JomMalaysia.Core.Mapping
{
    public class CoreDataProfile : Profile
    {
        public CoreDataProfile()
        {
            #region Listing Mapping
            CreateMap<Listing, ListingViewModel>()
                .ForMember(vm => vm.PublishStatus, opt => opt.MapFrom(l => l.PublishStatus))
                .ForMember(vm => vm.ListingType, opt => opt.MapFrom(l => l.ListingType.ToString()))
                .ForMember(vm => vm.Merchant, opt => opt.MapFrom(l => l.Merchant))
                .ForMember(vm => vm.ListingAddress, opt => opt.MapFrom(l => l.Address))
                .IncludeAllDerived()
            ;

            CreateMap<EventListing, ListingViewModel>()
                .ForMember(vm => vm.EventStartDateTime, opt => opt.MapFrom(l => l.EventStartDateTime))
                .ForMember(vm => vm.EventEndDateTime, opt => opt.MapFrom(l => l.EventEndDateTime))
                .IncludeBase<Listing, ListingViewModel>()
            ;

            CreateMap<LocalListing, ListingViewModel>()
           .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Category))
           .IncludeBase<Listing, ListingViewModel>()
               ;

            CreateMap<Merchant, MerchantViewModel>()
                .ForMember(vm => vm.SsmId, opt => opt.MapFrom(l => l.CompanyRegistration.SsmId))
                .ForMember(vm => vm.RegistrationName, opt => opt.MapFrom(l => l.CompanyRegistration.RegistrationName))
                ;

            CreateMap<Address, AddressViewModel>();

            CreateMap<StoreTimes, StoreTimesViewModel>()
            .ForMember(vm => vm.DayOfWeek, opt => opt.MapFrom(l => l.DayOfWeek.Id))
            .ForMember(vm => vm.OpenTime, opt => opt.MapFrom(l => l.OpenTime))
            .ForMember(vm => vm.CloseTime, opt => opt.MapFrom(l => l.CloseTime))
            ;

            CreateMap<PublishStatus, PublishStatusViewModel>()
            .ForMember(vm => vm.Status, opt => opt.MapFrom(l => l.Status.ToString()))
            ;
            #endregion


            #region map user
            CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.Name, opt => opt.MapFrom(u => u.Name.ToString()))
                .ForMember(vm => vm.Email, opt => opt.MapFrom(u => u.Email.ToString()))
                .ForMember(vm => vm.Role, opt => opt.MapFrom(u => u.Role.ToString().ToUpper()))
                .ForMember(vm => vm.HasAuthority, opt => opt.MapFrom(u => u.CanAssign))
            ;
            CreateMap<PagingHelper<User>, PagingHelper<UserViewModel>>()

              ;

            #endregion

            #region workflow to workflowvm
            CreateMap<Workflow, WorkflowViewModel>()

            ;

            CreateMap<Listing, ListingSummary>();

            CreateMap<User, UserVM>()
            ;

            CreateMap<Merchant, MerchantVM>()
            ;

            CreateMap<Workflow, WorkflowSummaryViewModel>();

            #endregion
        }
    }
}