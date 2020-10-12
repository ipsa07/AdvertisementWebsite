using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertiseSearchApi.Models;

namespace AdvertiseSearchApi.Services
{
    public interface IAdvertiseSearchService
    {
        Task<List<AdvertiseType>> SearchAdvertisement(string keyword);
    }
}
