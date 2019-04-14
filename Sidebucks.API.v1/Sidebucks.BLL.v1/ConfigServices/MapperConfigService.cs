using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sidebucks.DAL.v1.Entities.Mobile;
using Sidebucks.DAL.v1.Identity;
using Sidebucks.DAL.v1.Models;
using System;

namespace Sidebucks.BLL.v1.ConfigServices {
    public class MapperProfile : Profile {

        public MapperProfile() {
            CreateMap<CreateUserModel, ApplicationUser>()
                .ForMember(p => p.CreatedAt, f => f.ResolveUsing(p => DateTime.UtcNow))
                .ForMember(p => p.UpdatedAt, f => f.ResolveUsing(p => DateTime.UtcNow));

            CreateMap<AspNetUsers, UserDetails>();

            CreateMap<Skills, GetSkillsModel>();
        }
    }

    public static class MapperConfigService {
        public static IServiceCollection RegisterMapper(this IServiceCollection services) {

            services.AddAutoMapper();

            return services;
        }
    }
}
