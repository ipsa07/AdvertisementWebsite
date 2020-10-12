using System;

namespace AdvertiseApi.Models
{
    public class ConfirmAdvertiseModel
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public AdvertiseStatus Status { get; set; }
    }
}
