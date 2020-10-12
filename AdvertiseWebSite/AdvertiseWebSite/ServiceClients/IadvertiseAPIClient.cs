using System;
using System.Threading.Tasks;

namespace AdvertiseWebSite.ServiceClients
{
    public interface IadvertiseAPIClient
    {
        Task<AdvertiseResponseOfCreation> CreateAsync(CreateAdvertiseModel model);
    }
}
