using System.Collections.Generic;
using System.IO;
using MedicalApplication.DAL.Interfaces;
using MedicalApplication.DTO;
using MedicalApplication.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicalApplication.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMedicineDAL _medicineDAL;
        private static string FileName = "medicineData.json";
        private readonly ILogger<MedicineService> _logger;

        public MedicineService(IWebHostEnvironment webHostEnvironment, IMedicineDAL medicineDAL, ILogger<MedicineService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _medicineDAL = medicineDAL;
            _logger = logger;
        }
        public bool AddMedicine(MedicineDTO medicineDTO)
        {
            _logger.LogInformation("Inside AddMedicine");
            string filePath = GetFilePath();

            string jsonData = System.IO.File.ReadAllText(filePath);
            var list = JsonConvert.DeserializeObject<List<MedicineDTO>>(jsonData);
            list.Add(medicineDTO);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);

            bool result = _medicineDAL.AddMedicine(filePath, convertedJson);
            _logger.LogInformation("Inside AddMedicine:: Get the result from dal for saving the medicine data", result);

            return result;

        }

        /// <summary>
        /// fetches the medicine data
        /// </summary>
        /// <returns></returns>
        public List<MedicineDTO> GetMedicines()
        {
            _logger.LogInformation("Inside GetMedicines");
            List<MedicineDTO> medicineDTO = new List<MedicineDTO>();

            string filePath = GetFilePath();
            if (!File.Exists(filePath))
            {
                _logger.LogWarning("Inside GetMedicines::File does not exists with the given path");
                return medicineDTO;
            }

            string jsonMedicineData = _medicineDAL.GetMedicines(filePath);
            if (!string.IsNullOrEmpty(jsonMedicineData))
            {
                medicineDTO = JsonConvert.DeserializeObject<List<MedicineDTO>>(jsonMedicineData);
            }
            _logger.LogInformation("Inside GetMedicines:: Fetched the data from dal", jsonMedicineData);
            return medicineDTO;
        }

        /// <summary>
        /// return filepath
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            _logger.LogInformation("Inside GetFilePath");

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Json");
            string filePath = Path.Combine(path, FileName);

            _logger.LogInformation("Inside GetFilePath::File path is", filePath);

            return filePath;

        }

    }
}
