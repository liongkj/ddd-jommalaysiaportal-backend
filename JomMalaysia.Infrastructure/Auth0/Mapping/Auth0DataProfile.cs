using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
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
            //.ForMember(dest => dest.email, opt => opt.MapFrom(src => src.StringEmail))
            //.ForMember(dest => dest.name, opt => opt.MapFrom(src => src.StringName))
            .ForMember(dest => dest.nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.verify_email, opt => opt.MapFrom(src => src.VerifyEmail))
            .ForMember(dest => dest.connection, opt => opt.MapFrom(src => src.Connection))
            .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
            .ReverseMap();

            CreateMap<PagingHelper<User>, Auth0PagingHelper<UserDto>>(MemberList.None)
            .ForMember(dest => dest.users, opt => opt.MapFrom(src => src.Results))    
            .ReverseMap();
        }
    }
}
