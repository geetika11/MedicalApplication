using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApplication.Models
{
    public class APIResponseModel
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public dynamic Data { get; set; }
    }
}
