using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertiseApi.Models;

namespace AdvertiseApi.Services
{
    public interface IadvertiseStorageService
    {
        Task<string> Add(AdvertiseModel model);

        Task Confirm(ConfirmAdvertiseModel model);

        Task<AdvertiseModel> GetByIdAsync(string id);

        Task<bool> CheckHealthAsync();

        Task<List<AdvertiseModel>> GetAllAsync();
    }
}
