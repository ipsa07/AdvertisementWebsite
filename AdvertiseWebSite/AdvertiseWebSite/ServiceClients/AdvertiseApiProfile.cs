using System;
using AutoMapper;
using AdvertiseApi.Models;

namespace AdvertiseWebSite.ServiceClients
{
    public class AdvertiseApiProfile : Profile
    {
        CreateMap<AdvertiseModel, CreateAdvertiseModel>().ReverseMap();
        CreateMap<AdvertiseResponse, AdvertiseResponseOfCreation>().ReverseMap();
        CreateMap<ConfirmAdvertiseModel, ConfirmAdvertiseRequestModel>().ReverseMap();
    }
}
