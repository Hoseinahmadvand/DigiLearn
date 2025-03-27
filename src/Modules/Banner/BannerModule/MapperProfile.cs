using AutoMapper;
using BannerModule.Domain;
using BannerModule.Services.DTOs.Command;
using BannerModule.Services.DTOs.Query;

namespace BannerModule;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Banner,BannerDto>().ReverseMap();
        CreateMap<Banner,CreateBannerCommand>().ReverseMap();
        CreateMap<Banner,EditBannerCommand>().ReverseMap();
    }
}
