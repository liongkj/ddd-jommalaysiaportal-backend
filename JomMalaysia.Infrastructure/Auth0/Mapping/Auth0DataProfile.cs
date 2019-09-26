using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
using JomMalaysia.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JomMalaysia.Infrastructure.Auth0.Mapping
{
    public class Auth0DataProfile : Profile
    {
        public Auth0DataProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.ToString()))
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email.ToString()))

            ;

            CreateMap<Auth0User, User>(MemberList.None)
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => Email.For(src.email)))
                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.app_metadata.authorization.roles.FirstOrDefault()))
                        .ForMember(dest => dest.AdditionalPermissions, opt => opt.MapFrom(src => src.app_metadata.authorization.permissions))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Name.For(src.name)))
                        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.username))
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                        .ForMember(dest => dest.PictureUri, opt => opt.MapFrom(src => src.picture))
                        .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.last_login))


                        ;


            CreateMap<Auth0PagingHelper<Auth0User>, PagingHelper<User>>()
                            .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.users))
                            .ReverseMap();






            // CreateMap<PagingHelper<User>, Auth0PagingHelper<Auth0User>>(MemberList.None)
            // .ForMember(dest => dest.users, opt => opt.MapFrom(src => src.Results))
            // .ReverseMap();
        }
    }
}
