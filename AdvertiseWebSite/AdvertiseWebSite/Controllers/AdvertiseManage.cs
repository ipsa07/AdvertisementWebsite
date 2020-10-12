using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdvertiseApi.Models;
using AutoMapper;
using AdvertiseWebSite.Models.AdvertisementManagement;
using AdvertiseWebSite.ServiceClients;
using AdvertiseWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertiseWebSite.Controllers
{
    public class AdvertiseManage : Controller
    {
        private readonly IS3FileUpload _s3FileUploader;
        private readonly IadvertiseAPIClient _advertiseApiClient;
        private readonly IMapper _mapper;
        public AdvertiseManage(IS3FileUpload s3FileUploader, IadvertiseAPIClient advertiseApiClient, IMapper mapper)
        {
            _s3FileUploader = s3FileUploader;
            _advertiseApiClient = advertiseApiClient;
            _mapper = mapper;
        }
        public IActionResult Create(CreateAdvertisementManageViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementManageViewModel model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var createAdvertiseModel = _mapper.Map<CreateAdvertiseModel>(model);
                createAdvertiseModel.UserName = User.Identity.Name;

                var apiCallResponse = await _advertiseApiClient.CreateAsync(createAdvertiseModel).ConfigureAwait(false);
                var id = apiCallResponse.Id;

                bool isOkToConfirmAd = true;
                string filePath = string.Empty;
                if (imageFile != null)
                {
                    var fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    filePath = $"{id}/{fileName}";

                    try
                    {
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await _s3FileUploader.UploadFileAsync(filePath, readStream)
                                .ConfigureAwait(false);
                            if (!result)
                                throw new Exception(
                                    "Could not upload the image to file repository. Please see the logs for details.");
                        }
                    }
                    catch (Exception e)
                    {
                        isOkToConfirmAd = false;
                        var confirmModel = new ConfirmAdvertiseRequestModel()
                        {
                            Id = id,
                            FilePath = filePath,
                            Status = AdvertiseStatus.Pending
                        };
                        await _advertiseApiClient.ConfirmAsync(confirmModel);
                        Console.WriteLine(e);
                    }


                }

                if (isOkToConfirmAd)
                {
                    var confirmModel = new ConfirmAdvertiseRequestModel()
                    {
                        Id = id,
                        FilePath = filePath,
                        Status = AdvertiseStatus.Active
                    };
                    await _advertiseApiClient.ConfirmAsync(confirmModel).ConfigureAwait(false);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
    

