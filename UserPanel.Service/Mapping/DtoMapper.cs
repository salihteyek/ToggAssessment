using AutoMapper;
using UserPanel.Core.Dtos;
using UserPanel.Core.Models;

namespace UserPanel.Service.Mapping
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
        }
    }
}
