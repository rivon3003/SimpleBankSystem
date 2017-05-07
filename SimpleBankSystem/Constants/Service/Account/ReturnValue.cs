using SimpleBankSystem.Constants.Web;
using SimpleBankSystem.Models.ResponseModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Constants.Service.Account
{
    public class ReturnValue
    {
        public static TransactionResponseModel SuccessfulDeposit = new TransactionResponseModel
                    {
                        IsSuccessful = true,
                        CurrentBalance = 0,
                        Message = Message.SuccessfulTracsaction,
                        RowVersion = ""
    };
}
}
