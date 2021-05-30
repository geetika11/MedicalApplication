using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MedicalApplication.DTO;
using MedicalApplication.Models;
using MedicalApplication.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicalApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly ILogger<MedicineController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMedicineService _medicineService;
        public MedicineController(ILogger<MedicineController> logger, IWebHostEnvironment webHostEnvironment, IMedicineService medicineService)
        {
            _logger = logger;
            _medicineService = medicineService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// fetches the list of medicines 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getMedicineList")]
        public APIResponseModel GetMedicinesList()
        {
            APIResponseModel aPIResponse = new APIResponseModel();
            _logger.LogInformation("Inside GetMedicinesList:: Calling service to fetch the medicine data");

            try
            {
                var medicineList = _medicineService.GetMedicines();
                if (medicineList.Any())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<MedicineDTO, MedicineModel>());
                    var mapper = config.CreateMapper();
                    var medicineData = mapper.Map<List<MedicineModel>>(medicineList);

                    foreach (var item in medicineData)
                    {
                        if (item.Quantity < 10)
                        {
                            item.QuantityBGColor = "#ffff00";
                        }
                        if ((item.ExpiryDate.Date - DateTime.Now).TotalDays < 30)
                        {
                            item.ExpiryDateBGColor = "#ff0000";
                        }
                    }
                    aPIResponse.Data = medicineData;
                    aPIResponse.IsSuccess = true;
                }
                else
                {
                    _logger.LogInformation("Inside GetMedicinesList:: No medicine data is present");
                    aPIResponse.Message = "No medicine data is present";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Inside GetMedicinesList:: Error while fetching medicine list.", ex.Message);
                aPIResponse.Message = "Error while fetching medicine data.";
            }
            return aPIResponse;

        }

        /// <summary>
        /// saves the details of medicine
        /// </summary>
        /// <param name="medicineModel"></param>
        [HttpPost]
        [Route("saveMedicine")]
        public APIResponseModel SaveMedicineData([FromBody] MedicineModel medicineModel)
        {
            APIResponseModel aPIResponse = new APIResponseModel();
            _logger.LogInformation("Inside SaveMedicineData:: Calling service to save the medicine data");

            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MedicineModel, MedicineDTO>());
                var mapper = new Mapper(config);

                MedicineDTO dto = mapper.Map<MedicineDTO>(medicineModel); //converts the model into dto

                bool result = _medicineService.AddMedicine(dto);
                if (result)
                {
                    aPIResponse.IsSuccess = true;
                    aPIResponse.Message = "Successfully saved the medicine details";
                }
                else
                {
                    _logger.LogWarning("Inside SaveMedicineData::Internal Server error while saving the medicine details.");
                    aPIResponse.Message = "Internal Server error while saving the medicine details.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Inside SaveMedicineData::Error while saving medicine details.", ex.Message);
                aPIResponse.Message = "Error occurred while saving medicine details.";
            }
            return aPIResponse;
        }
    }
}
