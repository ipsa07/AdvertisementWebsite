using System;
using AdvertiseApi.Models;
namespace AdvertiseESWriter
{
    public static class Mapper
    {
        public static AdvertiseType Map(ConfirmedAdvertisementMsg message)
        {
            var doc = new AdvertiseType
            {
                Id = message.Id,
                Title = message.Title,
                CreationDateTime = DateTime.UtcNow
            };
            return doc;
        }
    }
}
