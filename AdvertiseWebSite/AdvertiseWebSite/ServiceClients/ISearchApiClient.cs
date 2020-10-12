using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertiseWebSite.Models;

namespace AdvertiseWebSite.ServiceClients
{
    public interface ISearchApiClient
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
