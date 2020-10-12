using System;
using AutoMapper;
using AdvertiseWebSite.Models;
using AdvertiseWebSite.Models.AdvertisementManagement;
using AdvertiseWebSite.ServiceClients;
using AdvertiseApi.Models;

namespace AdvertiseWebSite.Classes
{
    public class SiteProfile : Profile
    {
        public SiteProfile()
        {
            CreateMap<CreateAdvertiseModel, CreateAdvertisementManageViewModel>().ReverseMap();

            CreateMap<AdvertiseModel, Advertisement>().ReverseMap();

            CreateMap<Advertisement, IndexViewModel>()
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title))
                .ForMember(dest => dest.Image, src => src.MapFrom(field => field.FilePath));

            CreateMap<AdvertiseType, SearchViewModel>()
                .ForMember(
                    dest => dest.Id, src => src.MapFrom(field => field.Id))
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title));
        }
    }
}
