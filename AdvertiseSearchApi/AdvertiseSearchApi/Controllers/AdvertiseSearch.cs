using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdvertiseSearchApi.Services;
using AdvertiseSearchApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertiseSearchApi.Controllers
{
    [Route("api/[controller]")]
    public class AdvertiseSearch : ControllerBase
    {
        private readonly IAdvertiseSearchService _searchService;
        private readonly ILogger<AdvertiseSearch> _logger;

        public AdvertiseSearch(IAdvertiseSearchService searchService, ILogger<AdvertiseSearch> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{keyword}")]
        public async Task<List<AdvertiseType>> Get(string keyword)
        {
            _logger.LogInformation("Search method was called");
            return await _searchService.SearchAdvertisement(keyword);
        }
    }
}
