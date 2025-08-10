using AutoMapper;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.SERVICE.Dtos.Role;
using EXPERMIN.SERVICE.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            CreateMap<ApplicationRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<ApplicationUser, UserDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                    src.UserRoles != null
                    ? src.UserRoles.Select(ur => new RoleDto
                    {
                        Id = ur.Role != null ? ur.Role.Id : null,
                        Name = ur.Role != null ? ur.Role.Name : "Sin rol"
                    }).ToList()
                    : new List<RoleDto>()))
                .ReverseMap();

            CreateMap<UserUpdateDto, ApplicationUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ApplicationUserRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role != null ? src.Role.Id : null))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "Sin rol"))
                .ReverseMap();
        }
    }
}
