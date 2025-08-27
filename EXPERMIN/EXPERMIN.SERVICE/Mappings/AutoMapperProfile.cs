using AutoMapper;
using EXPERMIN.CORE.Extensions;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
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

            CreateMap<BannerUpdateDto, Banner>()
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status.HasValue))
                .ForMember(dest => dest.RouteType,
                    opt => opt.Condition(src => src.RouteType.HasValue))
                .ForMember(dest => dest.StatusDirection,
                    opt => opt.Condition(src => src.StatusDirection.HasValue))
                .ForMember(dest => dest.Order,
                    opt => opt.MapFrom((src, dest) =>
                        src.Order.HasValue ? (byte?)src.Order.Value : dest.Order))
                .ForMember(dest => dest.UrlDirection,
                    opt => opt.Condition(src => src.UrlDirection != null))
                .ForMember(dest => dest.NameDirection,
                    opt => opt.Condition(src => src.NameDirection != null))
                .ForMember(dest => dest.Headline,
                    opt => opt.Condition(src => src.Headline != null))
                .ForMember(dest => dest.Description,
                    opt => opt.Condition(src => src.Description != null));

            CreateMap<MediaFile, MediaFileDto>()
                .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => src.UploadDate.ToLocalDateTimeFormat()))
                .ReverseMap()
                .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => DateTime.Parse(src.UploadDate)));

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status.HasValue))
                .ForMember(dest => dest.Order,
                    opt => opt.MapFrom((src, dest) =>
                        src.Order.HasValue ? (byte?)src.Order.Value : dest.Order))
                .ForMember(dest => dest.Description,
                    opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.ShortDescription,
                    opt => opt.Condition(src => src.ShortDescription != null))
                .ForMember(dest => dest.Title,
                    opt => opt.Condition(src => src.Title != null));

            CreateMap<TestimonyUpdateDto, Testimony>()
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status.HasValue))
                .ForMember(dest => dest.Order,
                    opt => opt.MapFrom((src, dest) =>
                        src.Order.HasValue ? (byte?)src.Order.Value : dest.Order))
                .ForMember(dest => dest.Rating,
                    opt => opt.Condition(src => src.Rating.HasValue))
                .ForMember(dest => dest.Comment,
                    opt => opt.Condition(src => src.Comment != null))
                .ForMember(dest => dest.ClientName,
                    opt => opt.Condition(src => src.ClientName != null));

            CreateMap<CollaboratorUpdateDto, Collaborator>()
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status.HasValue))
                .ForMember(dest => dest.Order,
                    opt => opt.MapFrom((src, dest) =>
                        src.Order.HasValue ? (byte?)src.Order.Value : dest.Order))
                .ForMember(dest => dest.Name,
                    opt => opt.Condition(src => src.Name != null));
        }
    }
}
