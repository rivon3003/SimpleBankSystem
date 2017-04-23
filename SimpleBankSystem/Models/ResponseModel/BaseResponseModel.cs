using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.ResponseModel
{
    public class BaseResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
