using System;
using MedicalApplication.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedicalApplication.DAL
{
    public class MedicineDAL : IMedicineDAL
    {
        private readonly ILogger<MedicineDAL> _logger;
        public MedicineDAL(ILogger<MedicineDAL> logger)
        {
            _logger = logger;
        }

        public bool AddMedicine(string filePath, string medicineList)
        {
            _logger.LogInformation("Inside AddMedicine");
            
            try
            {
                System.IO.File.WriteAllText(filePath, medicineList);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Inside AddMedicine::Error occurred while saving medicine data to the file present at path {filePath}, error is {ex.Message}");
                return false;
            }
        }

        public string GetMedicines(string filePath)
        {
            _logger.LogInformation("Inside GetMedicines");
            
            try
            {
                string json = System.IO.File.ReadAllText(filePath);
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Inside GetMedicines:: Error while saving medicine data at path{filePath}, error is {ex.Message}");
                return string.Empty;
            }
        }
    }
}
