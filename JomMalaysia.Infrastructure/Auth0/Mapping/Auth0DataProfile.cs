using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
using JomMalaysia.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure.Auth0.Mapping
{
    public class Auth0DataProfile : Profile
    {
        public Auth0DataProfile()
        {
            CreateMap<User, UserDto>(MemberList.None)
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.ToString()))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email.ToString()))
            //.ForMember(dest => dest.role, opt => opt.Ignore())
            // .ForMember(dest => dest.user_id, opt => opt.Ignore())
            ;

            CreateMap<PagingHelper<User>, Auth0PagingHelper<UserDto>>(MemberList.None)
            .ForMember(dest => dest.users, opt => opt.MapFrom(src => src.Results))
            .ReverseMap();
        }
    }
}
