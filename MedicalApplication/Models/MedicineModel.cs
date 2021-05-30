using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApplication.Models
{
    public class MedicineModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
        public string QuantityBGColor { get; set; }
        public string ExpiryDateBGColor { get; set; }
    }
}
