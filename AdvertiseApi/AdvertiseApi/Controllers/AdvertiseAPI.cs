using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertiseApi.Models;
using AdvertiseApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Amazon.SimpleNotificationService;
using AdvertiseApi.Models.Messages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertiseApi.Controllers
{
    [Route("api/[controller]")]
    public class AdvertiseAPI : ControllerBase
    {
        private IadvertiseStorageService _iadvertiseStorageService;
        private IConfiguration _configuration;
        public AdvertiseAPI(IadvertiseStorageService iadvertiseStorageService, IConfiguration configuration)
        {
            _iadvertiseStorageService = iadvertiseStorageService;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(AdvertiseResponse))]


        public async Task<IActionResult> Create(AdvertiseModel model)
        {
            string recordId;
            try
            {
                recordId = await _iadvertiseStorageService.Add(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return StatusCode(201, new AdvertiseResponse { Id = recordId });
        }
        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertiseModel model)
        {
            try
            {
                await _iadvertiseStorageService.Confirm(model);
                await RaiseConfirmedAdvertisementMessage(model);

            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return new OkResult();
        }

        private async Task RaiseConfirmedAdvertisementMessage(ConfirmAdvertiseModel model)
        {
            var topicArn = _configuration.GetValue<string>("TopicArn");
            var dbModel = await _iadvertiseStorageService.GetByIdAsync(model.Id);

            using (var client = new AmazonSimpleNotificationServiceClient())
            {
                var message = new ConfirmedAdvertisementMsg
                {
                    Id = model.Id,
                    Title = dbModel.Title
                };

                var messageJson = JsonConvert.SerializeObject(message);
                await client.PublishAsync(topicArn, messageJson);
            }
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var advert = await _iadvertiseStorageService.GetByIdAsync(id);
                return new JsonResult(advert);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [EnableCors("AllOrigin")]
        public async Task<IActionResult> All()
        {
            return new JsonResult(await _iadvertiseStorageService.GetAllAsync());
        }
    }
}
