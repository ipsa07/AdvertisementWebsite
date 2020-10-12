using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertiseSearchApi.Models;
using Nest;
namespace AdvertiseSearchApi.Services
{
    public class AdvertiseSearchService
    {
        private readonly IElasticClient _client;
        public AdvertiseSearchService(IElasticClient client)
        {
            _client = client;
        }

        public async Task<List<AdvertiseType>> SearchAdvertisement(string keyword)
        {
            var searchResponse = await _client.SearchAsync<AdvertiseType>(search => search.
                Query(query => query.
                    Term(field => field.Title, keyword.ToLower())
                ));

            return searchResponse.Hits.Select(hit => hit.Source).ToList();

        }
    }
}
