using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Repository.Infrastructure;
using SimpleBankSystem.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace SimpleBankSystem.Services.Implementation
{
    public class AccountService : IAccountService
    {
        #region Contructor and Properties

        private readonly IUnitOfWorkFactory<IUnitOfWork> _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWorkFactory<IUnitOfWork> unitOfWorkFactory, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #endregion Contructor and Properties

        public void CreateAccount(RegisterViewModel registerVm)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var account = _mapper.Map<RegisterViewModel, Account>(registerVm);
                unit.Repository<Account>().Insert(account);
                unit.Save();
            }
        }

        public bool IsAccNumExisted(string accNum)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                return unit.Repository<Account>().Get().Any(a => a.AccountNumber == accNum);
            }
        }
    }
}
