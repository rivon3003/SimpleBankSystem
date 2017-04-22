using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Services.Infrastructure
{
    public interface IAccountService
    {
        void CreateAccount(RegisterViewModel account);
    }
}
