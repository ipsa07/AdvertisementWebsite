using System;
using AdvertiseApi.Models;
using AutoMapper;

namespace AdvertiseApi.Services
{
    public class AdvertiseProfile : Profile
    {
        public AdvertiseProfile()
        {
            CreateMap<AdvertiseModel, AdvertiseDBModel>();

        }
    }
}
