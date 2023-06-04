using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BlogAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                );
                
            CreateMap<User, RegisterResponseDto>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                );

            CreateMap<LoginRequestDto, User>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => src.Password)
                );
        }
            
    }
}