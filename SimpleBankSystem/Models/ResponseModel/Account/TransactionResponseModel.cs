using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.ResponseModel.Account
{
    public class TransactionResponseModel : BaseResponseModel
    {
        public decimal CurrentBalance { get; set; }
    }
}
