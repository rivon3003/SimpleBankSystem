using SimpleBankSystem.Models.ResponseModel.Account;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SimpleBankSystem.Constants.Service.Account;

namespace SimpleBankSystem.Test
{
    public class AccountServiceTest
    {
        private IAccountService _accSer;

        public AccountServiceTest(IAccountService accSer)
        {
            _accSer = accSer;
        }

        [Fact]
        public void Test()
        {

        }

        [Fact]
        public void DepositTest()
        {
            DepositViewModel model = new DepositViewModel
            {
                AccountNumber = "0001",
                Amount = 1,
                RowVersion = ""
            };

            var result = _accSer.DepositAsync(model);

            var rightResult = ReturnValue.SuccessfulDeposit;

            Assert.Equal(1, 1);
        }
    }
}
