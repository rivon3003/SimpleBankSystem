using Moq;
using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Services.Infrastructure;
using SimpleBankSystem.Repository.Infrastructure;
using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using SimpleBankSystem.Repository.Implementation;

namespace SimpleBankSystem.Test
{
    public class AccountServiceTest
    {
        [Fact]
        ///Step 1: T1 Read
        ///Step 2: T2 Read
        ///Step 3: T1 Take 1 from balance
        ///Step 4: T2 Add 2 to balance
        ///Step 5: T1 Save change to database
        ///Step 6: T2 Save change to database (if T2 can save data, we will lost the update of T1)
        public void ConcurencyTest_LostUpdates()
        {
            var accS = new Mock<IAccountService>();
            var unitOfWorkFac = new Mock<IUnitOfWorkFactory<IUnitOfWork>>();

            string accNum = "0001";
            List<Account> accounts = new List<Account>();
            unitOfWorkFac.Setup(uf => uf.Create().Repository<Account>().Get()).Returns(accounts.AsQueryable());

            var account = accounts.SingleOrDefault(a => a.AccountNumber == accNum);

            //Step 1: 
            var withdrawModel = new WithdrawViewModel
            {
                AccountNumber = account.AccountNumber,
                Amount = 1,
                RowVersion = Convert.ToBase64String(account.RowVersion)
            };

            //Step 2: 
            var depositModel = new DepositViewModel
            {
                AccountNumber = account.AccountNumber,
                Amount = 2,
                RowVersion = Convert.ToBase64String(account.RowVersion)
            };

            //Step 3: 
            accS.Setup(x => x.WithdrawAsync(withdrawModel));

            unitOfWorkFac.Setup(u => u.Create().SaveAsync());
            //Step 4:
            accS.Setup(x => x.WithdrawAsync(withdrawModel));
            unitOfWorkFac.Setup(u => u.Create().SaveAsync());
        }

        [Fact]
        public void ConcurencyTest_UncommittedData()
        {
            
        }

        [Fact]
        public void ConcurencyTest_InconsistentRetrievals()
        {

        }
    }
}
