using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalApplication.DTO;

namespace MedicalApplication.Service.Interfaces
{
   public interface IMedicineService
    {
        /// <summary>
        /// fetches the list of medicine
        /// </summary>
        /// <returns></returns>
        List<MedicineDTO> GetMedicines();

        /// <summary>
        /// save the medicine data
        /// </summary>
        /// <param name="medicineDTO"></param>
        /// <returns></returns>
        bool AddMedicine(MedicineDTO medicineDTO);
    }
}
