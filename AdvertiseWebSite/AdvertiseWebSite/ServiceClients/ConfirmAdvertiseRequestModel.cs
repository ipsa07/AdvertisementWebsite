using System;
using AdvertiseApi.Models;
namespace AdvertiseWebSite.ServiceClients
{
    public class ConfirmAdvertiseRequestModel
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public AdvertiseStatus Status { get; set; }
    }
}
