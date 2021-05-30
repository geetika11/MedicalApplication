using System;

namespace MedicalApplication.DTO
{
    public class MedicineDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string Notes { get; set; }
      
    }
}
