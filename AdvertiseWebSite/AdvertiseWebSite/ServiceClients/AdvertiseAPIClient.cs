using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AdvertiseApi.Models;
using System.Net;
using System.Collections.Generic;

namespace AdvertiseWebSite.ServiceClients
{
    public class AdvertiseAPIClient : IadvertiseAPIClient
    {
        private readonly string _baseAddress;
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdvertiseAPIClient(IConfiguration configuration, HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

            _baseAddress = configuration.GetSection("AdvertApi").GetValue<string>("BaseUrl");
        }

        public async Task<AdvertiseResponseOfCreation> CreateAsync(CreateAdvertiseModel model)
        {
            var advertiseApiModel = _mapper.Map<AdvertiseModel>(model);

            var jsonModel = JsonConvert.SerializeObject(advertiseApiModel);
            var response = await _client.PostAsync(new Uri($"{_baseAddress}/create"),
                new StringContent(jsonModel, Encoding.UTF8, "application/json"));
            var advertiseResponse = await response.Content.ReadAsAsync<AdvertiseResponse>();
            var advertiseResponseOfCreation = _mapper.Map<AdvertiseResponseOfCreation>(advertiseResponse);

            return advertiseResponseOfCreation;
        }

        public async Task<bool> ConfirmAsync(ConfirmAdvertiseRequestModel model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertiseModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await _client
                .PutAsync(new Uri($"{_baseAddress}/confirm"),
                    new StringContent(jsonModel, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            var apiCallResponse = await _client.GetAsync(new Uri($"{_baseAddress}/all")).ConfigureAwait(false);
            var allAdvertModels = await apiCallResponse.Content.ReadAsAsync<List<AdvertiseModel>>().ConfigureAwait(false);
            return allAdvertModels.Select(x => _mapper.Map<Advertisement>(x)).ToList();
        }

        public async Task<Advertisement> GetAsync(string advertId)
        {
            var apiCallResponse = await _client.GetAsync(new Uri($"{_baseAddress}/{advertId}")).ConfigureAwait(false);
            var fullAdvert = await apiCallResponse.Content.ReadAsAsync<AdvertiseModel>().ConfigureAwait(false);
            return _mapper.Map<Advertisement>(fullAdvert);
        }
    }
}
